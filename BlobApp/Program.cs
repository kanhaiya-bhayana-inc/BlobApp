using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=blobdemostgacc;AccountKey=GvE0EDTpo0H8uko5kBUP+sY13iRbdtVxsP5UAyjK8RT9VHpHiHAcoz7C+s5DFG9/xCszyChLEEbK+AStPq87uw==;EndpointSuffix=core.windows.net";
string containerName = "scripts";
string blobName = "script.txt";
string filePath = "C:\\test_data\\script.txt";


// BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
BlobClient blobClient1 = new BlobClient(connectionString, containerName, blobName);

try
{
    //await SetBlobMetaData();
    await GetMetaData();

    // --------- Create Container ----------
    // var res = await blobServiceClient.CreateBlobContainerAsync(containerName,PublicAccessType.None);
    // Console.WriteLine("Contaienr Created");
    
    

    // --------- Upload blob -------
    /*var res = await blobClient.UploadAsync(filePath, true);
    Console.WriteLine(res);
    Console.WriteLine("Blob Uploaded successfully");*/



    // ---------- Get all blobs ----------
    /*await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
    {
        Console.WriteLine($"Name: {blobItem.Name} and Size: {blobItem.Properties.ContentLength}");
    }*/


    // -------- Downlaod blob ------------
    // var res = await blobClient1.DownloadToAsync(filePath);
    // Console.WriteLine("Blob downloaded successfully.");

    // ----- Set meta Data -------
    async Task SetBlobMetaData()
    {
        string blobName = "script.txt";
        BlobClient blobClient2 = new BlobClient(connectionString, containerName, blobName);

        IDictionary<string,string> metaData = new Dictionary<string,string>();
        metaData.Add("Department", "Logistic");
        metaData.Add("Application", "AppA");
        
        await blobClient2.SetMetadataAsync(metaData);

        Console.WriteLine("Metadata added");


    }

    async Task GetMetaData()
    {
        string bloblName = "script.txt";
        BlobClient blobClient2 = new BlobClient(connectionString,containerName, blobName);
        BlobProperties blobProperties = await blobClient2.GetPropertiesAsync();

        foreach (var metaData in blobProperties.Metadata)
        {
            Console.WriteLine($"Key: {metaData.Key} ---- Value: {metaData.Value}");
        }
    }

}
catch(Exception ex)
{
    Console.WriteLine(ex.Message.ToString());
}