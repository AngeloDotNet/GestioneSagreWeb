namespace GestioneSagre.Web.Services;

public static class FrontendParameters
{
    //TODO: Change ENDPOINT_PATH to "http://api-gateway:5001" when deployed on docker
    private const string ENDPOINT_PATH = "http://sagre-api.aepserver.it"; //"http://api-gateway:5001";

    // GET methods
    public const string ENDPOINT_GET_HELLOWORLD = $"{ENDPOINT_PATH}/helloworld/";
    public const string ENDPOINT_GET_FESTE = $"{ENDPOINT_PATH}/getfeste";
    public const string ENDPOINT_GET_FESTA_ID = $"{ENDPOINT_PATH}/getfesta";
    public const string ENDPOINT_GET_INTESTAZIONE_IDFESTA = $"{ENDPOINT_PATH}/getintestazione";
    public const string ENDPOINT_GET_IMPOSTAZIONE_IDFESTA = $"{ENDPOINT_PATH}/getimpostazione";

    // POST methods
    public const string ENDPOINT_POST_FESTA = $"{ENDPOINT_PATH}/festa";
    public const string ENDPOINT_POST_INTESTAZIONE = $"{ENDPOINT_PATH}/intestazione";
    public const string ENDPOINT_POST_IMPOSTAZIONE = $"{ENDPOINT_PATH}/impostazione";

    // PUT methods
    public const string ENDPOINT_PUT_FESTA = $"{ENDPOINT_PATH}/festa";
    public const string ENDPOINT_PUT_INTESTAZIONE = $"{ENDPOINT_PATH}/intestazione";
    public const string ENDPOINT_PUT_IMPOSTAZIONE = $"{ENDPOINT_PATH}/impostazione";

    // DELETE methods
}