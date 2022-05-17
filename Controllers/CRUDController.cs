using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rd_ARTi_CRUD.Models;
using System.Text;

namespace Rd_ARTi_CRUD.Controllers
{
    public class CRUDController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        postsModel _crud = new postsModel();
        List<postsModel> _posts = new List<postsModel>();


        public CRUDController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<postsModel>> GetAllPosts()
        {
            _posts = new List<postsModel>();

            using(var httpClient = new HttpClient(_clientHandler))
            {
                using(var response=await httpClient.GetAsync("https://localhost:7141/api/posts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _posts = JsonConvert.DeserializeObject<List<postsModel>>(apiResponse);
                }
            }
            return _posts; 
        }


        [HttpGet]
        public async Task<postsModel> GetById(int id)
        {
            _crud = new postsModel();
            using (var HttpClient = new HttpClient(_clientHandler))
            {
                using (var response = await HttpClient.GetAsync("https://localhost:7141/api/posts/"+ id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _crud = JsonConvert.DeserializeObject<postsModel>(apiResponse);
                }
            }
            return _crud;
        }


        [HttpPost]
        public async Task<postsModel> AddUpdatePosts(postsModel post)
        {
            _crud = new postsModel();

            using (var HttpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

                using (var response = await HttpClient.PostAsync("https://localhost:7141/api/posts", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _crud = JsonConvert.DeserializeObject<postsModel>(apiResponse);
                }
            }
            return _crud;
        }

        [HttpGet]
        public async Task<string> Delete(int id)
        {
            string message = "";

            using (var HttpClient = new HttpClient(_clientHandler))
            {
                using (var response = await HttpClient.DeleteAsync("https://localhost:7141/api/posts/" + id))
                {
                    message = await response.Content.ReadAsStringAsync();
    
                }
            }
            return message;
        }
    }
}
