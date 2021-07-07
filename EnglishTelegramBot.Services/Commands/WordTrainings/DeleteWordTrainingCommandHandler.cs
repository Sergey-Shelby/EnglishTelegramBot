using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Commands.WordTrainings
{
	public class DeleteWordTrainingCommandHandler : ICommandHandler<DeleteWordTrainingCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		public DeleteWordTrainingCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task Handle(DeleteWordTrainingCommand command)
		{
			var wordTrainings = await _unitOfWork.WordTrainingRepository.FetchBySetAsync(command.WordTrainingSetId);
			foreach (var wordTraining in wordTrainings)
			{
				await _unitOfWork.WordTrainingRepository.DeleteAsync(wordTraining.Id);
			}
			await _unitOfWork.WordTrainingSetRepository.DeleteAsync(command.WordTrainingSetId);
		}
	}
}
