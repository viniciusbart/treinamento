using System;
using System.Collections.Generic;
using System.Linq;
using GAtec.Northwind.Domain.Business;
using GAtec.Northwind.Domain.Model;
using GAtec.Northwind.Domain.Repository;

namespace GAtec.Northwind.Business
{
    public class CategoryService : ICategoryService
    {
        public IDictionary<string, string> Validation { get; private set; }

        private ICategoryRepository CategoryRepository { get; set; }

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
            this.Validation = new Dictionary<string, string>();
        }

        public void Add(Category category)
        {
            //Exemplo de implementação de Exception
            //if (string.IsNullOrEmpty(category.Name))
            //{
            //    throw new Exception("The name is empty.");
            //}

            if (!IsValid(category))
                return; //return vazio = exit sub VB

            CategoryRepository.Add(category);
        }

        public void Update(Category category)
        {
            if (!IsValid(category))
                return; //return vazio = exit sub VB

            CategoryRepository.Update(category);
        }

        public void Delete(int id)
        {
            CategoryRepository.Delete(id);
        }

        public Category GetCategory(int id)
        {
            var category = CategoryRepository.Get(id);

            return category;
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = CategoryRepository.GetAll();

            return categories;
        }

        private bool IsValid(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                Validation.Add("Name", "The name is required.");
            }
            else if (CategoryRepository.ExistsName(category.Name, category.Id))
            {
                Validation.Add("Name", "The name already exists.");
            }
            return !Validation.Any();
        }
    }
}