using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using GoToWorkDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GoToWorkDatabaseImplement.Implements
{
    public class ProductStorage : IProductStorage
    {
        public List<ProductViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Products
                    .Include(rec => rec.ProductWorkers)
                    .ThenInclude(rec => rec.Worker)
                    .Include(rec => rec.ProductParts)
                    .ThenInclude(rec => rec.Part)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<ProductViewModel> GetFilteredList(ProductBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                return context.Products
                    .Include(rec => rec.ProductWorkers)
                    .ThenInclude(rec => rec.Worker)
                    .Include(rec => rec.ProductParts)
                    .ThenInclude(rec => rec.Part)
                    .Where(rec => rec.Name == model.Name)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public ProductViewModel GetElement(ProductBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                var product = context.Products
                    .Include(rec => rec.ProductWorkers)
                    .ThenInclude(rec => rec.Worker)
                    .Include(rec => rec.ProductParts)
                    .ThenInclude(rec => rec.Part)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return product != null ?
                    CreateModel(product) :
                    null;
            }
        }
        public void Insert(ProductBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Product(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(ProductBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var product = context.Products.FirstOrDefault(rec => rec.Id == model.Id);

                        if (product == null)
                        {
                            throw new Exception("Изделие не найдено");
                        }

                        CreateModel(model, product, context);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(ProductBindingModel model)
        {
            using (var context = new Database())
            {
                var product = context.Products.FirstOrDefault(rec => rec.Id == model.Id);

                if (product == null)
                {
                    throw new Exception("Изделие не найдено");
                }

                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        private ProductViewModel CreateModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,

                ProductWorkers = product.ProductWorkers
                            .ToDictionary(rec => rec.WorkerId,
                            rec => (rec.Worker?.Name, rec.Count)),
                ProductParts = product.ProductParts
                            .ToDictionary(rec => rec.PartId,
                            rec => (rec.Part?.Name, rec.Count))
            };
        }

        private Product CreateModel(ProductBindingModel model, Product product, Database context)
        {
            product.Name = model.Name;
            product.Cost = model.Cost;

            if (product.Id == 0)
            {
                context.Products.Add(product);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var productWorkers = context.ProductWorkers
                    .Where(rec => rec.ProductId == model.Id.Value)
                    .ToList();

                context.ProductWorkers.RemoveRange(productWorkers.ToList());

                var productParts = context.ProductParts
                   .Where(rec => rec.ProductId == model.Id.Value)
                   .ToList();

                context.ProductParts.RemoveRange(productParts.ToList());

                context.SaveChanges();
            }

            foreach (var productWorker in model.ProductWorkers)
            {
                context.ProductWorkers.Add(new ProductWorker
                {
                    ProductId = product.Id,
                    WorkerId = productWorker.Key,
                    Count = productWorker.Value.Item2
                });
                context.SaveChanges();
            }

            foreach (var productParts in model.ProductParts)
            {
                context.ProductParts.Add(new ProductPart
                {
                    ProductId = product.Id,
                    PartId = productParts.Key,
                    Count = productParts.Value.Item2
                });
                context.SaveChanges();
            }
            return product;
        }
    }
}
