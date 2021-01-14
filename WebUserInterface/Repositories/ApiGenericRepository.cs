using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebUserInterface.Models;

namespace WebUserInterface.Repositories
{
    public class ApiGenericRepository<TEntity> : IApiGenericRepository<TEntity>
        where TEntity : IBaseEntity
    {
        private readonly Uri Uri;
        private HttpClient Client;

        public ApiGenericRepository(Uri uri)
        {
            Uri = uri;
            InitializeHttpClient();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(Uri);
                response.EnsureSuccessStatusCode();  // Throw exception if not success code

                return await response.Content.ReadAsAsync<IEnumerable<TEntity>>();
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TEntity> GetAsync(int id)
        {
            Uri uri = new Uri(Uri + "/" + id);
            //return await GetAsync(uri);

            TEntity entity = default;
            HttpResponseMessage response = await Client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                entity = await response.Content.ReadAsAsync<TEntity>();
            }
            return entity;
        }

        public async Task<TEntity> PostAsync(TEntity entity)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync(Uri, entity);
                response.EnsureSuccessStatusCode();  // Throw exception if not success code

                return await response.Content.ReadAsAsync<TEntity>();
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task PutAsync(TEntity entity)
        {
            Uri uri = new Uri(Uri + "/" + entity.Id);
            await PutAsync(uri, entity);
        }

        public async Task DeleteAsync(int id)
        {
            Uri uri = new Uri(Uri + "/" + id);
            await DeleteAsync(uri);
        }

        private async Task<TEntity> GetAsync(Uri uri)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(uri);
                response.EnsureSuccessStatusCode();  // Throw exception if not success code

                return await response.Content.ReadAsAsync<TEntity>();
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task PutAsync(Uri uri, TEntity entity)
        {
            try
            {
                var response = await Client.PutAsJsonAsync(uri, entity);
                response.EnsureSuccessStatusCode();  // Throw exception if not success code
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task DeleteAsync(Uri uri)
        {
            try
            {
                var response = await Client.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();  // Throw exception if not success code
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }

        private void InitializeHttpClient()
        {
            // Initialize HttpClient
            Client = new HttpClient();
            Client.BaseAddress = Uri;
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}