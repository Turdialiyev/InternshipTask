namespace InternshipTask.Repositories;

public interface IUnitOfWork
{
    IProductRepository Products {get; set;}
    IProductHistoryRepository ProductRepositories { get; set; }
}