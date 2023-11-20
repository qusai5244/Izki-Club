using Izki_Club.Data;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Helpers;
using Izki_Club.Models;
using Izki_Club.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Services
{
    public class PersonService : IPersonService
    {
        private readonly DataContext _context;
        public PersonService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ViewPersonDto>> CreatePerson(AddPersonDto input)
        {
            try
            {
                var person = Mapper.PersonDtoToPerson(input);

                _context.Persons.Add(person);

                await _context.SaveChangesAsync();

                var personDto = Mapper.PersonToPersonDto(person);

                return new ApiResponse<ViewPersonDto>(true, 200, "Person created successfully", personDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Creating Person: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewPersonDto>(false, 500, $"An error occurred in Creatint Person: {ex}", null);
            }
        }


        public async Task<ApiResponse<ViewPersonDto>> GetPerson(int Id)
        {
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (person == null)
                {
                    return new ApiResponse<ViewPersonDto>(false, 404, "Person not found", null);
                }

                var personDto = Mapper.PersonToPersonDto(person);

                return new ApiResponse<ViewPersonDto>(true, 200, "Person retrived succssefuly", personDto);

            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Creating Person: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewPersonDto>(false, 500, $"An error occurred in retriving Person: {ex}", null);
            }

        }

        public async Task<ApiResponse<PaginatedList<ViewPersonDto>>> GetPersons(string searchInput, PersonType personType, int page, int pageSize)
        {

            try
            {
                var persons = _context.Persons.AsQueryable();

                if (!string.IsNullOrEmpty(searchInput))
                {
                    persons = persons.Where(x => x.NameEn.Contains(searchInput) || x.NameAr.Contains(searchInput));
                }

                //var personsCount = await players.Where(x => !x.IsDeleted).CountAsync();

                var personsToReturn = await persons
                    .Where(x => !x.IsDeleted && x.PersonType == personType)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(player => Mapper.PersonToPersonDto(player))
                    .ToListAsync();

                var paginatedList = new PaginatedList<ViewPersonDto>(personsToReturn, personsToReturn.Count(), page, pageSize);

                return new ApiResponse<PaginatedList<ViewPersonDto>>(true, 200, "Persons retrived succssefuly", paginatedList);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Get Persons: {ex}");

                return new ApiResponse<PaginatedList<ViewPersonDto>>(false, 404, "Failed to retrived Persons Data", null);
            }

        }

        public async Task<ApiResponse<ViewPersonDto>> UpdatePerson(UpdatePersonDto input)
        {
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == input.Id && !x.IsDeleted);

                if (person == null)
                {
                    return new ApiResponse<ViewPersonDto>(false, 404, "Person not found", null);
                }

                // Delete the old image after checking if the user is found
                ImageProcess.DeleteImage(person.ImageUrl);

                // Update user information
                person.PersonType = input.PersonType;
                person.NameAr = input.NameAr;
                person.NameEn = input.NameEn;
                person.DescriptionAr = input.DescriptionAr;
                person.DescriptionEn = input.DescriptionEn;
                person.DateOfBirth = input.DateOfBirth;
                person.ImageUrl = ImageProcess.UploadImage(input.Image);
                person.UpdatedAt = DateTime.Now;

                // Save changes to the database

                _context.Persons.Update(person);
                await _context.SaveChangesAsync();

                // Return ApiResponse with a success status code and updated player information
                var updatedPersonDto = Mapper.PersonToPersonDto(person);
                return new ApiResponse<ViewPersonDto>(true, 200, "Person updated successfully", updatedPersonDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Update Person: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewPersonDto>(false, 500, $"An error occurred in Update Person: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewPersonDto>> DeletePerson(int Id)
        {
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (person == null)
                {
                    return new ApiResponse<ViewPersonDto>(false, 404, "Person not found", null);
                }

                person.IsActive = false;
                person.IsDeleted = true;

                _context.Persons.Update(person);

                await _context.SaveChangesAsync();

                var personDto = Mapper.PersonToPersonDto(person);

                return new ApiResponse<ViewPersonDto>(true, 200, "Person deleted successfully", personDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Delete Person: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewPersonDto>(false, 500, $"An error occurred in Delete Person: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewPersonDto>> UpdatePersonStatus(int Id)
        {
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (person == null)
                {
                    return new ApiResponse<ViewPersonDto>(false, 404, "Person Status Updated successfully", null);
                }

                person.IsActive = !person.IsActive;

                person.UpdatedAt = DateTime.Now;

                _context.Persons.Update(person);

                await _context.SaveChangesAsync();

                var personDto = Mapper.PersonToPersonDto(person);

                return new ApiResponse<ViewPersonDto>(true, 200, "Person Status updated successfully", personDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Changing Person Status: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewPersonDto>(false, 500, $"An error occurred in updating Person Status : {ex}", null);
            }
        }

    }
}
