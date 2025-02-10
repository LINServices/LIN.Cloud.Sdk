using LIN.Cloud.SDK.Models;
using LIN.Types.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LIN.Cloud.SDK;

public class Client(Options options)
{

    /// <summary>
    /// Cargar un archivo.
    /// </summary>
    /// <param name="file">Archivo.</param>
    /// <param name="path">Carpeta.</param>
    /// <param name="progress">Acción de progreso.</param>
    public async Task<ReadOneResponse<UploadResult>> Upload(byte[] file, string path, Action<double> progress, bool filePublic = false)
    {

        // Nueva URL.
        var uri = new Uri(options.Url);
        var complete = new Uri(uri, "file");

        var client = new Global.Http.Services.Client(complete.ToString())
        {
            // Minutos.
            TimeOut = 60 * options.MinutesTimeOut
        };

        using var content = new MultipartFormDataContent();
        var byteContent = new ByteArrayContent(file);
        byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");

        content.Add(byteContent, "modelo", "name");

        // Parámetros.
        client.AddHeader("key", options.Key);
        client.AddParameter("path", path);
        client.AddParameter("public", filePublic);
        client.AddParameter("aleatoryName", true);

        // Enviar solicitud.
        var response = await client.Post<ReadOneResponse<UploadResult>>(content);

        return response;
    }


    /// <summary>
    /// Cargar un archivo.
    /// </summary>
    /// <param name="fileStream">Stream.</param>
    /// <param name="path">Carpeta.</param>
    /// <param name="progress">Acción de progreso.</param>
    public async Task<ReadOneResponse<UploadResult>> Upload(Stream fileStream, string path, Action<double> progress, bool filePublic = false)
    {

        // Nueva URL.
        var uri = new Uri(options.Url);
        var complete = new Uri(uri, "file");

        var client = new Global.Http.Services.Client(complete.ToString())
        {
            // Minutos.
            TimeOut = 60 * options.MinutesTimeOut
        };

        // Parámetros.
        client.AddHeader("key", options.Key);
        client.AddParameter("path", path);
        client.AddParameter("public", filePublic);
        client.AddParameter("aleatoryName", true);

        // Enviar solicitud.
        var response = await client.Post<ReadOneResponse<UploadResult>>(fileStream, "fileName", progress);

        return response;
    }


    /// <summary>
    /// Obtener ruta publica para un archivo.
    /// </summary>
    /// <param name="file">Archivo.</param>
    public async Task<ReadOneResponse<List<string>>> GetPublic(string file, int minutes = 10)
    {

        var uri = new Uri(options.Url);
        var complete = new Uri(uri, "PublicFiles/token");

        var client = new Global.Http.Services.Client(complete.ToString())
        {
            // Segundos.
            TimeOut = 60
        };

        client.AddHeader("key", options.Key);
        client.AddParameter("path", file);
        client.AddParameter("minutes", minutes);

        var response = await client.Get<ReadOneResponse<List<string>>>();

        return response;
    }

}