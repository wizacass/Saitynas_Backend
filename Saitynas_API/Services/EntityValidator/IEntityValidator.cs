namespace Saitynas_API.Services.EntityValidator
{
    public interface IEntityValidator
    {
        public bool IsWorkplaceIdValid(int? id);
        public bool IsWorkplaceIdValid(int id);

        public bool IsSpecialityIdValid(int? id);
        public bool IsSpecialityIdValid(int id);
    }
}
