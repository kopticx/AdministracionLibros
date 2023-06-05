using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace AdministracionLibros.Servicios
{
    public class ServicioCloudinary : IServicioCloudinary
    {
        private readonly IConfiguration configuration;
        private Cloudinary cloudinary;

        public ServicioCloudinary(IConfiguration configuration)
        {
            this.configuration = configuration;
            Account account = new Account(
            configuration["cloudinaryKeys:CloudName"],
            configuration["cloudinaryKeys:ApiKey"],
            configuration["cloudinaryKeys:ApiSecret"]);

            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImageLibro(string urlImagen)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(urlImagen),
                Transformation = new Transformation().Width(628).Height(1025)
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            var url = uploadResult.Url.ToString();

            return url;
        }

        public async Task<string> UploadImageAutor(string urlImagen)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(urlImagen),
                Transformation = new Transformation().Width(2190).Height(3291)
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            var url = uploadResult.Url.ToString();

            return url;
        }

        public async Task<string> UploadImageUser(string urlImagen)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(urlImagen),
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            var url = uploadResult.Url.ToString();

            return url;
        }
    }
}
