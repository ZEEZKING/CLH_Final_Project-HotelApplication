using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;
using System.Text;

namespace CLH_Final_Project.Payment
{
    public class PayStackPayment : IPayStackPayment
    {
        private static HttpClient client;
        const string secretKey = "sk_test_828a49b4cf501708842b19bea69ccfd9a22793a3";
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentRepository  _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrderRepository _orderRepository;

        public PayStackPayment(ICustomerRepository customerRepository, IPaymentRepository paymentRepository, IBookingRepository bookingRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            _orderRepository = orderRepository;
            client = new HttpClient();
        }

        public async Task<string> GetTransactionRecieptAsync(string transactionReference)
        {
            if (transactionReference == null)
            {
                return null;
            }
            var transaction = await _paymentRepository.GetAsync(transactionReference);
            if (transaction == null)
            {
                return null;
            }
            string url = $"https://api.paystack.co/transaction/verify/{transactionReference}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {secretKey}");
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                // Transaction verification successful
                // Process the response content, which includes payment details

                // Deserialize the response JSON
                // var transaction = JsonConvert.DeserializeObject<dynamic>(responseContent);
                // Console.WriteLine(transaction);
                // Access the receipt information
                // var receiptUrl = transaction.data.receipt.url;
                // var receiptNumber = transaction.data.receipt.number;

                // Do further processing with the receipt information
                var transactionDto = new PaymentReferenceDto
                {
                    PhoneNumber = transaction.Customer.User.PhoneNumber,
                    FullName = transaction.Customer.User.Name.Split(" ")[0],
                    //LastName = transaction.Customer.FullName.Split(" ")[0],
                    ResponseContent = responseContent
                };
                return responseContent;
            }
            else
            {
                // Transaction verification failed
                throw new Exception($"Transaction verification failed. Response: {responseContent}");
            }
        }

        public async Task<string> InitiatePayment(CreatePaymentRequestModel model, int userId, int bookingId, int? orderId)
        {
            var customer = await _customerRepository.GetCustomerByUserIdAsync(userId);
            var order = await _orderRepository.GetAsync(x => x.Id == orderId);
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (customer == null && order == null && booking == null)
            {
                return null;
            }
            // Set your PayStack API test secret key

            // Set the API endpoint URL
            string url = "https://api.paystack.co/transaction/initialize";
            //string url = "https://104.18.7.191/transaction/initialize";

            // Set reciever account details
            var recipients = new
            {
                account_number = "7051459639",
                bank_code = "995",  // Bank code for the receiver's bank (e.g., GTBank)
                                    // Add any other recipient details as required
            };


            // Set the request payload
            var payload = new
            {
                amount = model.Amount * 100,  // Set the amount in kobo (e.g., 5000 = ₦5000)
                email = model.Email,
                phone = model.PhoneNumber,
                reference = Guid.NewGuid().ToString(),
                callback_url = $"http://127.0.0.1:5501/FrontEnd/dashboard/receipt.html?id={orderId}",
                Name = customer.User.Name.Split(" "),
               

            };

            // Serialize the payload to JSON
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            // Create the HTTP request
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            request.Headers.Add("Authorization", $"Bearer {secretKey}");

            // Send the request and retrieve the response
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Process the response
            if (response.IsSuccessStatusCode)
            {
                // Payment initiation successful
                var payment = new PaymentReference
                {
                    //OrderId = (int)orderId,
                    OrderId = orderId,
                    CustomerId = customer.Id,
                    BookingId = booking.Id,
                    Amount = model.Amount,
                    ReferenceNumber = responseContent.Split("\"reference\":")[1].Split("\"")[1],
                };
               /* Console.WriteLine($"Response Content: {responseContent}");
                Console.WriteLine($"Payment Amount: {payment.Amount}");*/
                await _paymentRepository.CreateAsync(payment);
                await _paymentRepository.SaveChangesAsync();
                return responseContent;
            }
            else
            {
                // Payment initiation failed
                throw new Exception($"Payment initiation failed. Response: {responseContent}");
            }
        }
    }
}
