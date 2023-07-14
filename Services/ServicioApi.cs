using APPEVENTOS.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APPEVENTOS.Services
{
    public class ServicioApi : IServicioApi
    {
        private static string Url = "https://10.0.2.2:7211/api/";

        public ServicioApi()
        {
        }

        public async Task<string> EditarUsuario(string cedula, Usuario cliente)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient clienteHttp = new HttpClient(httpClientHandler);
            clienteHttp.BaseAddress = new Uri(Url);
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PutAsync("Usuarios/" + cedula, content);
            if (response.IsSuccessStatusCode)
            {
                var json_respoonse = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respoonse);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }

        public async Task<string> GuardarUsuario(Usuario cliente)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient clienteHttp = new HttpClient(httpClientHandler);
            clienteHttp.BaseAddress = new Uri(Url);
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PostAsync("Usuarios", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }

        public async Task<string> GuardarVenta(Venta venta)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient clienteHttp = new HttpClient(httpClientHandler);
            clienteHttp.BaseAddress = new Uri(Url);
            var content = new StringContent(JsonConvert.SerializeObject(venta), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PostAsync("Ventas/guardar", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }

        public async Task<List<Evento>> ListarEventos()
        {
            List<Evento> ev = new List<Evento>();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient clienteHttp = new HttpClient(httpClientHandler);
            clienteHttp.BaseAddress = new Uri(Url);
            var response = await clienteHttp.GetAsync("Eventoes");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                ev = resultado.LEvento;
            }
            return ev;
        }

        public async Task<Usuario> LoginUsuario(Usuario usuario)
        {
            if (usuario != null)
            {

                Usuario nu = new Usuario();
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                HttpClient clienteHttp = new HttpClient(httpClientHandler);
                clienteHttp.BaseAddress = new Uri(Url);
                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                var response = await clienteHttp.PostAsync("Access", content);

                if (response.IsSuccessStatusCode)
                {
                    var json_response = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                    nu = resultado.NUsuario;
                    return nu;
                }
                else { return null; }

            }
            return null;
        }

        public async Task<List<DatosCompra>> ObtenerCompras(string cedula)
        {
            List<DatosCompra> ven = new List<DatosCompra>();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient clienteHttp = new HttpClient(httpClientHandler);
            clienteHttp.BaseAddress = new Uri(Url);
            var response = await clienteHttp.GetAsync("Ventas/compra/" + cedula);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                List<Venta> v = resultado.LVenta;
                List<Evento> e = resultado.LEvento;
                foreach (var item in v)
                {
                    DatosCompra dc = new DatosCompra();
                    Evento ee = new Evento();
                    ee = e.ElementAt(0);
                    e.RemoveAt(0);
                    dc.Id = item.Id;
                    dc.cantidad = item.cantidad;
                    dc.fechaEvento = ee.fecha;
                    dc.fechaCompra = item.fecha;
                    dc.titulo = ee.titulo;
                    dc.descripcion = ee.descripcion;
                    dc.ubicacion = ee.ubicacion;
                    dc.costo = ee.costo;
                    ven.Add(dc);
                }
            }
            return ven;
        }

        public async Task<DatosCompra> ObtenerVenta(int id)
        {
            DatosCompra ven = new DatosCompra();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient clienteHttp = new HttpClient(httpClientHandler);
            clienteHttp.BaseAddress = new Uri(Url);
            var response = await clienteHttp.GetAsync("Ventas/Ven/" + id);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                Venta v = resultado.NVenta;
                Evento e = resultado.NEvento;
                ven.Id = v.Id;
                ven.cantidad = v.cantidad;
                ven.fechaEvento = e.fecha;
                ven.fechaCompra = v.fecha;
                ven.titulo = e.titulo;
                ven.descripcion = e.descripcion;
                ven.ubicacion = e.ubicacion;
                ven.costo = e.costo;
            }
            return ven;
        }
        public async Task<string> EliminarUsuario(string cedula)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient clienteHttp = new HttpClient(httpClientHandler);
            clienteHttp.BaseAddress = new Uri(Url);
            var response = await clienteHttp.DeleteAsync("Usuarios/" + cedula);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }
    }
}
