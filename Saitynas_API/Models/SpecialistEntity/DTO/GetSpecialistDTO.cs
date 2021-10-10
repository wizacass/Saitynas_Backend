namespace Saitynas_API.Models.SpecialistEntity.DTO
{
    public class GetSpecialistDTO : SpecialistDTO
    {
        public int Id { get; set; }

        public GetSpecialistDTO() { }

        public GetSpecialistDTO(Specialist s) : base(s)
        {
            Id = s.Id;
        }
    }
}
