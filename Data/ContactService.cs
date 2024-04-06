using CrudOperation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class ContactService
    {
        private readonly List<Contact> _contacts = new List<Contact>();

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            await Task.Delay(100); 
            return _contacts.ToList();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            await Task.Delay(100);
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public async Task AddContactAsync(Contact contact)
        {
            await Task.Delay(100); 
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

       
            contact.Id = _contacts.Count > 0 ? _contacts.Max(c => c.Id) + 1 : 1;

            _contacts.Add(contact);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await Task.Delay(100);
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            var existingContact = _contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact == null)
                throw new KeyNotFoundException($"Contact with ID {contact.Id} not found.");

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.PhoneNumber = contact.PhoneNumber;
        }

        public async Task DeleteContactAsync(int id)
        {
            await Task.Delay(100); 
            var contactToDelete = _contacts.FirstOrDefault(c => c.Id == id);
            if (contactToDelete == null)
                throw new KeyNotFoundException($"Contact with ID {id} not found.");

            _contacts.Remove(contactToDelete);
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
        {
            await Task.Delay(100); 
            return _contacts.Where(c =>
                c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.PhoneNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

    }
}
