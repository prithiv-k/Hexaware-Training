create database VirtualArtGallery
use VirtualArtGallery

-- Create the Artists table
create table Artists (
    ArtistID int primary key,
    Name varchar(255) NOT NULL,
    Biography text,
    Nationality varchar(100)
);

-- Create the Categories table
create table Categories (
    CategoryID int primary key,
    Name varchar(100) NOT NULL
);

-- Create the Artworks table
create table Artworks (
    ArtworkID int primary key,
    Title varchar(255) NOT NULL,
    ArtistID int,
    CategoryID int,
    Year int,
    Description text,
    ImageURL varchar(255),
    FOREIGN KEY (ArtistID) REFERENCES Artists (ArtistID) ,
    FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID)
);
-- create the exhibitions table
create table Exhibitions (
    ExhibitionID int primary key,
    Title varchar(255) not null,
    StartDate date,
    EndDate date,
    Description text
);

-- create a table to associate artworks with exhibitions
create table ExhibitionArtWorks (
    ExhibitionID int,
    ArtworkID int,
    primary key (ExhibitionID, ArtworkID),
    foreign key (ExhibitionID) references Exhibitions (ExhibitionID),
    foreign key (ArtworkID) references Artworks (ArtworkID)
);

--DML(Insert sample data to tables)

-- Insert sample data into the Artists table
INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES
 (1, 'Pablo Picasso', 'Renowned Spanish painter and sculptor.', 'Spanish'),
 (2, 'Vincent van Gogh', 'Dutch post-impressionist painter.', 'Dutch'),
 (3, 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', 'Italian');
 Select * from Artists

 -- Insert sample data into the Categories table
INSERT INTO Categories (CategoryID, Name) VALUES
 (1, 'Painting'),
 (2, 'Sculpture'),
 (3, 'Photography');
 Select * from Categories

 ---- Insert sample data into the Artworks table
INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
 (1, 'Starry Night', 2, 1, 1889, 'A famous painting by Vincent van Gogh.', 'starry_night.jpg'),
 (2, 'Mona Lisa', 3, 1, 1503, 'The iconic portrait by Leonardo da Vinci.', 'mona_lisa.jpg'),
 (3, 'Guernica', 1, 1, 1937, 'Pablo Picasso\s powerful anti-war mural.', 'guernica.jpg');

-- Insert sample data into the Exhibitions table
INSERT INTO Exhibitions (ExhibitionID, Title, StartDate, EndDate, Description) VALUES
 (1, 'Modern Art Masterpieces', '2023-01-01', '2023-03-01', 'A collection of modern art masterpieces.'),
 (2, 'Renaissance Art', '2023-04-01', '2023-06-01', 'A showcase of Renaissance art treasures.');

-- Insert artworks into exhibitions
INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES
 (1, 1),
 (1, 2),
 (1, 3),
 (2, 2); Select * from Artworks Select * from Exhibitions Select * from ExhibitionArtWorks --1.Retrieve the names of all artists along with the number of artworks they have in the gallery, and list them in descending order of the number of artworks.
select a.name, count(aw.artworkid) as artwork_count
from artists a
left join artworks aw 
on a.artistid = aw.artistid
group by a.name
order by artwork_count desc;

--2.List the titles of artworks created by artists from 'Spanish' and 'Dutch' nationalities, and order them by the year in ascending order.
select aw.title
from artworks aw
join artists a on aw.artistid = a.artistid
where a.nationality in ('spanish', 'dutch')
order by aw.year asc;

--3.Find the names of all artists who have artworks in the 'Painting' category, and the number of artworks they have in this category.
select a.name, count(aw.artworkid) as artwork_count
from artists a
join artworks aw on a.artistid = aw.artistid
join categories c on aw.categoryid = c.categoryid
where c.name = 'painting'
group by a.name;

--4.List the names of artworks from the 'Modern Art Masterpieces' exhibition, along with their artists and categories.
select aw.title as ArtworkName,e.title, a.name as artist, c.name as category
from artworks aw
join artists a on aw.artistid = a.artistid
join categories c on aw.categoryid = c.categoryid
join exhibitionartworks ea on aw.artworkid = ea.artworkid
join exhibitions e on ea.exhibitionid = e.exhibitionid
where e.title = 'modern art masterpieces';

--5.Find the artists who have more than two artworks in the gallery.
select a.ArtistID,a.name,count(a.name) as ContributedWorks
from Artists a 
join Artworks aw
on a.ArtistID=aw.ArtistID
group by a.Name,a.ArtistID
having count(a.name)>2
select * from Artists
Select * from Artworks

--6.Find the titles of artworks that were exhibited in both 'Modern Art Masterpieces' and 'Renaissance Art' exhibitions
select aw.title
from artworks aw
join exhibitionartworks ea1 on aw.artworkid = ea1.artworkid
join exhibitions e1 on ea1.exhibitionid = e1.exhibitionid
join exhibitionartworks ea2 on aw.artworkid = ea2.artworkid
join exhibitions e2 on ea2.exhibitionid = e2.exhibitionid
where e1.title = 'modern art masterpieces' and e2.title = 'renaissance art';

--7.Find the total number of artworks in each category
select c.name as category, count(a.artworkid) as total_artworks
from categories c
left join artworks a on c.categoryid = a.categoryid
group by c.categoryid, c.name;

--8.List artists who have more than 3 artworks in the gallery
Select a.Name,count(aw.ArtworkID)as total from Artists a
join Artworks as aw
on a.ArtistID=aw.ArtistID
group by aw.ArtworkID,a.name
having count(aw.ArtworkID) > 3

--9.Find the artworks created by artists from a specific nationality (e.g., Spanish).
select a.Name,a.Nationality,aw.title from Artists a
join Artworks aw
on a.ArtistID=aw.ArtistID
where Nationality='Spanish'

--10. List exhibitions that feature artwork by both Vincent van Gogh and Leonardo da Vinci.select e.title
from exhibitions e
join exhibitionartworks ea on e.exhibitionid = ea.exhibitionid
join artworks aw on ea.artworkid = aw.artworkid
join artists a on aw.artistid = a.artistid
where a.name in ('vincent van gogh', 'leonardo da vinci')
group by e.exhibitionid, e.title
having count(distinct a.Name)=2
Select * from Exhibitions
 select * from ExhibitionArtWorks
 select * from Artists

 --11. Find all the artworks that have not been included in any exhibition.
 select aw.title
from artworks aw
left join exhibitionartworks ea on aw.artworkid = ea.artworkid
where ea.artworkid is null;

--12. List artists who have created artworks in all available categories.select a.name
from artists a
join artworks aw on a.artistid = aw.artistid
group by a.artistid, a.name
having count(distinct aw.categoryid) = (select count(categoryid) from categories);

--13.List the total number of artworks in each category
select c.name as category, count(a.artworkid) as total_artworks
from categories c
left join artworks a on c.categoryid = a.categoryid
group by c.categoryid, c.name;

--14. Find the artists who have more than 2 artworks in the gallery
select c.name as category, count(a.artworkid) as total_artworks
from categories c
left join artworks a on c.categoryid = a.categoryid
group by c.categoryid, c.name
having count(a.artworkId)>2

--15.List the categories with the average year of artworks they contain, only for categories with more than 1 artwork
select c.name as category, avg(a.year) as average_year
from categories c
join artworks a on c.categoryid = a.categoryid
group by c.categoryid, c.name
having count(a.artworkid) > 1;

--16.Find the artworks that were exhibited in the 'Modern Art Masterpieces' exhibition
select a.title as artworks from artworks a
join exhibitionartworks ea on a.artworkid = ea.artworkid
join exhibitions e on ea.exhibitionid = e.exhibitionid
where e.title = 'Modern Art Masterpieces';

--17. Find the categories where the average year of artworks is greater than the average year of all artworks
select avg(year) from artworks
select c.name as category, avg(a.year) as average_year
from categories c
join artworks a on c.categoryid = a.categoryid
group by c.categoryid, c.name
having avg(a.year) >all (select avg(year) from artworks);
select * from Artworks

--18. List the artworks that were not exhibited in any exhibition.
select a.title 
from artworks a
left join exhibitionartworks ea on a.artworkid = ea.artworkid
where ea.artworkid is null;

--19.Show artists who have artworks in the same category as "Mona Lisa."
select * from Artists
select * from Artworks
select a.name 
from artists a
join artworks aw on a.artistid = aw.artistid
where aw.categoryid = (select categoryid from artworks where title = 'Mona Lisa');

--20.List the names of artists and the number of artworks they have in the gallery
select a.name, count(aw.artworkid) as total_artworks
from artists a
left join artworks aw on a.artistid = aw.artistid
group by a.artistid, a.name;
















