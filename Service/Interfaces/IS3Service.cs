namespace Service.Interfaces
{
    public interface IS3Service
    {
        public void EnviarArquivo(string bucketName, string url, MemoryStream arquivo);

        public void GetArquivo(string bucketName, string url, MemoryStream arquivo);

        public string GetTemplate(string template);
    }
}
