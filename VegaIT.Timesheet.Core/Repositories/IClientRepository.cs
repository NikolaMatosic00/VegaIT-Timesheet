using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Core.Repositories;

public interface IClientRepository
{
    void Create(Client client);
    PageOf<Client> FindPageByFirstLetterAndNameSubstring(string firstLetter, String nameSubstring, int pageSize, int pageNumber);
    void Edit(Client client);
    void DeleteById(Guid id);//Da li treba by id - da li bi trebao uopste na frontend da vracam id iz baze
    List<Client> FindAll(); //Za dropdown kreiranje novog projetka
    Maybe<Client> FindById(string id);
}
