using System;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.Entities.Patient.DTO;

namespace Saitynas_API.Services.Validators;

public interface IPatientDTOValidator
{
    public void ValidatePatientDTO(PatientDTO dto);
}

public class PatientDTOValidator : DTOValidator, IPatientDTOValidator
{
    public void ValidatePatientDTO(PatientDTO dto)
    {
        ValidateString(dto.FirstName, "firstName");
        ValidateString(dto.LastName, "lastName");
        ValidateString(dto.City, "city");

        if (!DateOnly.TryParse(dto.BirthDate, out var date))
        {
            throw new DTOValidationException(ApiErrorSlug.InvalidDateFormat, "birthDate");
        }
    }
}
