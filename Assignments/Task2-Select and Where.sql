--1.Listing all the customers
Select * from UserTable

--2.List all orders for a specific customer.
-- Replace 'Raj' with the specific customer name
select * 
from Courier 
where SenderName = 'Raj' OR ReceiverName = 'Raj';  

--3.List all couriers.
Select * from Courier

--4.List all packages for a specific order.
--schema does not include a separate Package table, meaning that each order corresponds directly to a row in the Courier table.
--If want to list all details for a specific order, we need to filter based on the CourierID or TrackingNumber.
Select * from Courier where CourierID=1  

--5.List all deliveries for a specific courier.
select *  from courier where courierid = 4;  

--6.List all undelivered packages.
select *  from courier where status != 'delivered';

--7.List all packages that are scheduled for delivery today.
select * from courier where deliverydate = convert(date, getdate());

--8.List all packages with a specific status.
select * from courier where status='Delivered';

--9.Calculate the total number of packages for each courier.
select courierid, count(*) as total_packages from courier 
group by courierid;

--10.Find the average delivery time for each courier.
select courierid, avg(datediff(day, getdate(), deliverydate)) as avg_delivery_time 
from courier 
group by courierid;

--11.List all packages with a specific weight range.
select * from Courier where Weight between 1 and 3

--12.Retrieve employees whose names contain ‘John’.instaed of John we can take name as 
select * from employee where name like '%raj%';

--13.Retrieve all courier records with payments greater than $50.
select c.*,p.amount from Courier as c
join Payment as p
on p.CourierID=c.CourierID
where p.Amount>50;











