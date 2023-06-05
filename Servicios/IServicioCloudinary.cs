namespace AdministracionLibros.Servicios
{
    public interface IServicioCloudinary
    {
        Task<string> UploadImageAutor(string urlImagen);
        Task<string> UploadImageLibro(string urlImagen);
        Task<string> UploadImageUser(string urlImagen);
    }
}
