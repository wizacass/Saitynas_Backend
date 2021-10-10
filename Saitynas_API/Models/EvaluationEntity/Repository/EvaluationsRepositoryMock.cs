using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_API.Models.EvaluationEntity.Repository
{
    public class EvaluationsRepositoryMock: IEvaluationsRepository
    {
        private readonly List<Evaluation> _evaluations;

        private int _lastInserted = 0;

        public EvaluationsRepositoryMock()
        {
            _evaluations = new List<Evaluation>();
           _evaluations.AddRange(new []
           {
              new Evaluation
              {
                  Id = ++_lastInserted,
                  Value = 5,
                  Comment = "Very good!",
                  SpecialistId = 1,
                  PatientId = 1
              },
              new Evaluation
              {
                  Id = ++_lastInserted,
                  Value = 3,
                  SpecialistId = 1,
                  PatientId = 2
              }
           });
        }
        
        public async Task<IEnumerable<Evaluation>> GetAllAsync()
        {
            var evaluations = new List<Evaluation>(_evaluations);
            return evaluations;
        }

        public async Task<Evaluation> GetAsync(int id)
        {
            try
            {
                return _evaluations[id];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task InsertAsync(Evaluation data)
        {
            var evaluation = new Evaluation
            {
                Id = ++_lastInserted,
                Comment = data.Comment,
                Value = data.Value,
                Patient = data.Patient,
                PatientId = data.PatientId,
                Specialist = data.Specialist,
                SpecialistId = data.SpecialistId
            };
            
            _evaluations.Add(evaluation);
        }

        public async Task UpdateAsync(int id, Evaluation data)
        {
            _evaluations[data.Id] = data;
        }

        public async Task DeleteAsync(int id)
        {
            var data = _evaluations.FirstOrDefault(e => e.Id == id);
            _evaluations.Remove(data);
        }
    }
}
