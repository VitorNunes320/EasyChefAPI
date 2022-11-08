using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Domain.Exceptions;
using Domain.Helpers;
using Microsoft.Extensions.Options;
using Service.Interfaces;

namespace Service.Services
{
    public class S3Service : IS3Service
    {
        private readonly BucketSettings _bucketSettings;

        public S3Service(IOptions<BucketSettings> bucketSettings)
        {
            _bucketSettings = bucketSettings.Value;
        }

        /// <summary>
        /// Envia um arquivo para o bucket
        /// </summary>
        /// <param name="bucketName">Nome do bucket</param>
        /// <param name="url">Chave do arquivo (caminho + "/" + nome do arquivo)</param>
        /// <param name="arquivo">Arquivo</param>
        /// <returns></returns>
        public void EnviarArquivo(string bucketName, string url, MemoryStream arquivo)
        {
            try
            {
                using (var client = BuscarAWSClient())
                {
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = url
                    };

                    putRequest.InputStream = arquivo;
                    PutObjectResponse response = client.PutObjectAsync(putRequest).Result;
                }
            }
            catch (Exception)
            {
                throw new EnviarArquivoS3Exception();
            }
        }

        public void GetArquivo(string bucketName, string url, MemoryStream arquivo)
        {
            using (var client = BuscarAWSClient())
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = url
                };
                using (GetObjectResponse response = client.GetObjectAsync(request).GetAwaiter().GetResult())
                using (Stream responseStream = response.ResponseStream)
                {
                    responseStream.CopyTo(arquivo);
                }
            }
        }

        public string GetTemplate(string template)
        {
            try
            {
                using (var client = BuscarAWSClient())
                {
                    var getRequest = new GetObjectRequest
                    {
                        BucketName = _bucketSettings.Bucket,
                        Key = template
                    };
                    using (GetObjectResponse response = client.GetObjectAsync(getRequest).Result)
                    using (Stream responseStream = response.ResponseStream)
                    {
                        var reader = new StreamReader(responseStream);
                        return reader.ReadToEnd();
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        private AmazonS3Client BuscarAWSClient()
        {
            return new AmazonS3Client(_bucketSettings.AccessKey, _bucketSettings.SecretKey, RegionEndpoint.USEast1);
        }
    }
}
