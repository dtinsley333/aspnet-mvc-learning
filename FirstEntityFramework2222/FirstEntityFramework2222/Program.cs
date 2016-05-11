using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstEntityFramework2222.Models;

namespace FirstEntityFramework2222
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var chinookContext = new ChinookContext())
            {
                var germanCustomers1 = chinookContext.Customer
                    .Where(cust => cust.Country.ToLower() == "germany");

                //first and last name of customer from canada order by last name
                var canadianCustomers = chinookContext.Customer
                    .Where(cust => cust.Country.ToLower() == "canada")
                    .OrderBy(a => a.LastName)
                    .Select(cust => new { cust.LastName, cust.FirstName });


                //get all customers 
                var allCustomers = chinookContext.Customer.ToList();


                //get all customers from germany 
                var germanCustomers = allCustomers.Where(cust => cust.Country.ToLower() == "germany");

                //get all customers from the USA
                var americanCustomers = allCustomers.Where(cust => cust.Country.ToLower().Contains("usa"));



                var americanAndGermanCustomers = allCustomers
                    .Where(cust => cust.Country.ToLower() == "germany"
                    || cust.Country.ToLower() == "usa");


                //bring back 100 artist order by name
                var first100Artist = chinookContext.Artist.Take(100).OrderBy(a => a.Name);
                //Is there a genre for TV?
                //var isThereATVShowGenre = chinookContext.Genre.Where(a => a.Name.Contains("TV"));

                //get my favorite artist and info
                var favoriteArtistAlbums = (from artist in chinookContext.Artist
                                            join album in chinookContext.Album
                                            on artist.ArtistId equals album.ArtistId
                                            where artist.ArtistId == 152
                                            orderby artist.Name
                                            select new
                                            {
                                                Artist = artist.Name,
                                                Album = album.Title
                                            }).ToList();
                Console.WriteLine();
            }

        }
    }
}
