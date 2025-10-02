
using LIN.Cloud.SDK;

Client client = new(new Options()
{
    Key = " key.iQ6PX2bnrqfZQWgBq7MQD619tCpnu9k"
});

Console.WriteLine("Nombre del archivo:");
string name = Console.ReadLine() ?? string.Empty;

Console.WriteLine("Carpeta:");
string path = Console.ReadLine() ?? string.Empty;

using var fileStream =  new FileStream("C:\\Users\\giral\\Downloads\\uu.jpg", FileMode.Open, FileAccess.Read);

var response = await client.Upload(fileStream, path, (e) => { });

Console.WriteLine(response.Response.ToString());
Console.WriteLine(response.Model.Name);
Console.WriteLine(response.Model.PublicPath);

// Obtener token publico.
var @public = await client.GetPublic(response.Model.Name);

Console.WriteLine($"https://cloud.api.linplatform.com/publicFiles/{@public.Model[0]}");

Console.ReadLine();