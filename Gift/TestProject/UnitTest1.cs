using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


[TestFixture]
public class Tests
{
    private HttpClient _httpClient;
    private string _generatedToken;
    [SetUp]
    public void Setup()
    {
         _httpClient = new HttpClient();
        // _httpClient.BaseAddress = new Uri("https://localhost:7266"); 
        // _httpClient.BaseAddress = new Uri("https://api.example.com/"); 
        _httpClient.BaseAddress = new Uri("https://8080-bfdeeddcedfabcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/"); 


    }

    // [Test]
    // public void Test1()
    // {
    //     Assert.Pass();
    // }

    
[Test, Order(1)]
public async Task Backend_TestRegisterUser()
{
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"customer\"}}";
    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    Console.WriteLine(response.StatusCode);
    string responseString = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseString);

    // Assuming you get a 200 OK status for successful registration
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
}

// [Test, Order(2)]
// public async Task Backend_TestLoginAdmin()
// {
//     string uniqueId = Guid.NewGuid().ToString();

//     string uniqueUsername = $"abcd_{uniqueId}";
//     string uniquePassword = $"abcdA{uniqueId}@123";
//     string uniqueEmail = $"abcd{uniqueId}@gmail.com";
//     string uniqueRole = "admin";

//     // Register a new admin user
//     string registerRequestBody = $"{{\"Username\" : \"{uniqueUsername}\",\"Password\" : \"{uniquePassword}\",\"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"{uniqueRole}\" }}";
//     HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

//     // Login with the registered admin user
//     string loginRequestBody = $"{{\"Email\" : \"{uniqueUsername}\",\"Password\" : \"{uniquePassword}\"}}";
//     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

//     // Additional assertions or actions based on the login response
//     string responseBody = await loginResponse.Content.ReadAsStringAsync();
//     // Add your assertions or further processing based on the login response body
// }


[Test, Order(2)]
public async Task Backend_TestRegisterAdmin()
{
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"admin\"}}";
    
    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    string responseBody = await response.Content.ReadAsStringAsync();
    // Add your assertions based on the response if needed
}


[Test, Order(5)]
        public async Task Backend_TestAddGiftAdmin()
        {
            // Create a unique title for the gift
            string uniqueTitle = Guid.NewGuid().ToString();
            string uniqueGiftTitle = $"giftTitle_{uniqueTitle}";

            // Construct the request body for adding a gift
            string giftJson = $"{{\"GiftType\":\"{uniqueGiftTitle}\",\"GiftImageUrl\":\"test\",\"GiftDetails\":\"test\",\"GiftPrice\":10,\"Quantity\":1}}";

            // Set the Authorization header with a valid admin token
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetAdminToken());

            // Perform the request to add a gift
            HttpResponseMessage response = await _httpClient.PostAsync("/api/Gift", new StringContent(giftJson, Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Optionally, you can check the response body for further assertions
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

            // Add your assertions based on the response if needed
            Assert.IsNotNull(responseMap);
        }



    [TearDown]
    public void TearDown()
    {
        // Cleanup or additional teardown logic if needed.
        _httpClient.Dispose();
    }
}