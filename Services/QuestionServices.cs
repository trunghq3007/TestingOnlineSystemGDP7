﻿using DataAccessLayer;
using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Services
{
    public class QuestionServices : Interfaces.IQuestionServices<Question>
    {
        private IQuestionRepository<Question> repository;

        public QuestionServices()
        {
            repository = new QuestionRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return repository.Delete(id);
        }
        public IEnumerable<Question> Filter(Question t)
        {
            return null;
        }
        public IEnumerable<Question> Filter(QuestionFillterModel t)
        {
            return repository.Filter(t);
        }


        public IEnumerable<Question> GetAll()
        {
            return repository.GetAll();
        }

        public Question GetById(int id)
        {
            return repository.GetById(id);
        }

        public int Insert(Question t)
        {
            return repository.Insert(t);
        }

        public IEnumerable<Question> Search(SearchPaging item)
        {
            return repository.Search(item);
        }

        public int Update(Question t)
        {
            return repository.Update(t);
        }
        public Category getCategoryByName(string cateName)
        {
            return repository.getCategoryByName(cateName);
        }
        public int Import(List<Question> list)
        {
            return repository.Import(list);
        }

    }
}
