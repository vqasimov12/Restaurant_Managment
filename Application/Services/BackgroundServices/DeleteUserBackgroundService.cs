using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Common;

namespace Application.Services.BackgroundServices;

public class DeleteUserBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider=serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var usersToDelete = unitOfWork.UserRepository.GetAll()
                    .Where(z => z.CreatedDate == null && !z.IsDeleted)
                    .ToList();

                if (usersToDelete.Count != 0)
                {
                    foreach (var user in usersToDelete)
                    {
                        user.IsDeleted = true;
                        user.DeletedDate = DateTime.Now;
                        user.DeletedBy = 1;
                    }
                    await unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}