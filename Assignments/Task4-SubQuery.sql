--49. Find couriers that have a weight greater than the average weight of all couriers
Select avg(weight) from Courier
Select * from Courier
where Weight>(Select avg(Weight) from Courier)

--50. Find the names of all employees who have a salary greater than the average salary
Select avg(salary) from Employee
Select name,Salary from Employee
where salary> (select avg(salary) from Employee)

--51. Find the total cost of all courier services where the cost is less than the maximum cost 
 Select sum(cost) as totalcost from CourierServices
 where cost<(select max(cost) from CourierServices) 

 --52. Find all couriers that have been paid for 
 select c.*  
from courier c  
join payment p on c.courierid = p.courierid;

--53. Find the locations where the maximum payment amount was made select location.locationname  
from location  
join payment on location.locationid = payment.locationid  
where payment.amount = (select max(amount) from payment);

--54. Find all couriers whose weight is greater than the weight of all couriers sent by a specific sender(e.g., 'SenderName')
Select weight from Courier where SenderName='Raj'
select * from Courierwhere weight>all(Select weight from Courier where SenderName='Raj')
