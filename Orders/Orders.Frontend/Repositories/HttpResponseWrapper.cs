﻿using System.Net;

namespace Orders.Frontend.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
                return null;

            var statusCode = HttpResponseMessage.StatusCode;

            if (statusCode == HttpStatusCode.NotFound) 
            {
                return "Recurso no encontrado.";
            }

            if (statusCode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }

            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Debe estar logeado para acceder al recurso.";
            }

            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tiene permiso para acceder al recurso.";
            }

            return "Ha ocurrido un error inesperado.";
        }
    }
}
