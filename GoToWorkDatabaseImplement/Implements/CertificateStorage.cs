using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using GoToWorkDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToWorkDatabaseImplement.Implements
{
    public class CertificateStorage : ICertificateStorage
    {
        public List<CertificateViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Certificates
                    .Include(rec => rec.CertificateProducts)
                    .ThenInclude(rec => rec.Product)
                    .Include(rec => rec.Engineer)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<CertificateViewModel> GetFilteredList(CertificateBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                return context.Certificates
                    .Include(rec => rec.CertificateProducts)
                    .ThenInclude(rec => rec.Product)
                    .Include(rec => rec.Engineer)
                    .Where(rec => (rec.EngineerId == model.EngineerId || (rec.Date >= model.DateFrom && rec.Date <= model.DateTo)))
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public CertificateViewModel GetElement(CertificateBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                var certificate = context.Certificates
                    .Include(rec => rec.CertificateProducts)
                    .ThenInclude(rec => rec.Product)
                    .Include(rec => rec.Engineer)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return certificate != null ?
                    CreateModel(certificate) :
                    null;
            }
        }
        public void Insert(CertificateBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Certificate(), context);
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
        public void Update(CertificateBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var certificate = context.Certificates.FirstOrDefault(rec => rec.Id == model.Id);

                        if (certificate == null)
                        {
                            throw new Exception("Акт не найден");
                        }

                        CreateModel(model, certificate, context);
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
        public void Delete(CertificateBindingModel model)
        {
            using (var context = new Database())
            {
                var certificate = context.Certificates.FirstOrDefault(rec => rec.Id == model.Id);

                if (certificate == null)
                {
                    throw new Exception("Акт не найден");
                }

                context.Certificates.Remove(certificate);
                context.SaveChanges();
            }
        }
        private CertificateViewModel CreateModel(Certificate certificate)
        {
            return new CertificateViewModel
            {
                Id = certificate.Id,
                EngineerId = certificate.EngineerId,
                Name = certificate.Name,
                Cost = certificate.Cost,
                Date = certificate.Date,
                EngineerName = certificate.Engineer.FIO,
                CertificateProducts = certificate.CertificateProducts
                            .ToDictionary(rec => rec.ProductId,
                            rec => rec.Product?.Name)
            };

        }

        private Certificate CreateModel(CertificateBindingModel model, Certificate certificate, Database context)
        {
            certificate.Name = model.Name;
            certificate.Cost = model.Cost;
            certificate.Date = model.Date;
            certificate.EngineerId = model.EngineerId;

            if (certificate.Id == 0)
            {
                context.Certificates.Add(certificate);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var certificateProduct = context.CertificateProduct
                     .Where(rec => rec.CertificateId == model.Id.Value)
                     .ToList();

                context.CertificateProduct.RemoveRange(certificateProduct.ToList());

                context.SaveChanges();
            }

            foreach (var certificateProduct in model.CertificateProducts)
            {
                context.CertificateProduct.Add(new CertificateProduct
                {
                    CertificateId = certificate.Id,
                    ProductId = certificateProduct.Key
                });
                context.SaveChanges();
            }
            return certificate;
        }
    }
}
