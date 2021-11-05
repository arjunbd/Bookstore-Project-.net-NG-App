create database bookstoreDB1

use bookstoreDB1

drop table tbl_adminCredentials;
drop table tbl_bookOrders;
drop table tbl_books;
drop table tbl_category;
drop table tbl_coupons;
drop table tbl_shoppingCart;
drop table tbl_userDetails;
drop table tbl_wishlist;
drop table tbl_bookKartList;


-----------------------------Admin Table----------------------------------------------------------------------------------------
create table tbl_adminCredentials(adminUserName varchar(20), adminPassword binary(32) not null,
								  constraint pk_adminUserName primary key (adminUserName));

select * from tbl_adminCredentials;
---------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------User Details Table------------------------------------------------------------------
create table tbl_userDetails(userName varchar(20), userPassword varchar(20) not null, userStatus bit not null, shippingAddress varchar(50),
							 constraint pk_userName primary key (userName));

insert into tbl_userDetails values('John', '1234', 1, 'address 1');

select * from tbl_userDetails;
----------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------Cupon Table-----------------------------------------------------------------------------------
							
create table tbl_coupons(couponId int identity, couponName varchar(20) not null, couponDiscount float not null,
						 constraint pk_couponId primary key (couponId));

insert into tbl_coupons values('10% discount', 0.1);

select * from tbl_coupons;
------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------Category Table---------------------------------------------------------------
create table tbl_category(categoryId int identity, categoryName varchar(20) not null, categoryDescription varchar(50), categoryImage varbinary(max), categoryStatus bit not null, categoryPosition varchar(20) not null, categoryCreatedAt datetime,
						  constraint pk_categoryId primary key (categoryId),
						  constraint uk_categoryName unique (categoryName),
						  constraint uk_categoryPosition unique(categoryPosition));

insert into tbl_category values('Fiction', 'Fictional Books', NULL, 1, '1', getdate());
insert into tbl_category values('Comics', 'Comic Books', NULL, 1, '2', getdate());


select * from tbl_category;
----------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------Books Table---------------------------------------------------------------------------------------------------
create table tbl_books(bookId int identity, categoryId int, bookTitle varchar(20) not null, bookISBN varchar(17) not null, bookAuthor varchar(20),
						bookYear smallint, bookPrice float not null, bookDescription varchar(50), bookPosition varchar(20) not null, 
						bookStatus bit not null, bookImage varbinary(max),

					    constraint pk_bookId primary key (bookId),
						constraint fk_categoryId foreign key (categoryId) references tbl_category);

insert into tbl_books values(1,'Harry Potter', '9780747532743','J K Rowling', 1997, 1000.0, 'Harry Potter is a wizard', '1', 1, NULL);
insert into tbl_books values(1, 'Sherlock Holmes', '9780747532744','J K Rowling', 1950, 500.0, 'Harry Potter is a wizard', '1', 1, NULL);
insert into tbl_books values(1, 'Percy Jackson', '9780747532745','J K Rowling', 1997, 2000.0, 'Harry Potter is a wizard', '1', 1, NULL);
insert into tbl_books values(1, 'Foundation', '9780747532746','J K Rowling', 1997, 2500.0, 'Harry Potter is a wizard', '1', 1, NULL);
insert into tbl_books values(1, 'Witcher', '9780747532743','J K Rowling', 1997, 1500.0, 'Harry Potter is a wizard', '1', 1, NULL);
insert into tbl_books values(2, 'Batman', '9780747532743','J K Rowling', 1997, 100.0, 'Harry Potter is a wizard', '1', 1, NULL);
insert into tbl_books values(2, 'Avengers', '183193','J K Rowling', 2000, 500, 'sakdjf', '1', 1, NULL);
insert into tbl_books values(1,'QWERTY', '465465144','J K Rowling', 1997, 1000.0, 'Harry Potter is a wizard', '1', 0, NULL);
insert into tbl_books values(1,'QWERTYASDD', '4654653254','J K Rowling', 1997, 1000.0, 'Harry Potter is a wizard', '1', 0, NULL);
insert into tbl_books values(1,'QWEadadcvsdf', '4654653292','Simon Curtis', 1997, 1000.0, 'Harry Potter is a wizard', '1', 0, NULL);

select * from tbl_books;

-----------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------Shopping Cart--------------------------------------------------------------------

create table tbl_shoppingCart(cartId int identity, userName varchar(20) not null ,orderStatus bit,
						    constraint pk_cartId primary key (cartId),
							constraint fk_userName foreign key (userName) references tbl_userDetails
							);

alter table tbl_shoppingCart add constraint df_orderStatus default 0 for orderStatus

insert into tbl_shoppingCart values('John',0);

select * from tbl_shoppingCart;
-----------------------------------------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------Orders Table--------------------------------------
create table tbl_bookOrders(orderId int identity, cartId int, couponId int, totalCost float not null,
						    constraint pk_orderId primary key (orderId),
							constraint fk_cartId foreign key (cartId) references tbl_shoppingCart,
							constraint fk_couponId foreign key (couponId) references tbl_coupons);

select * from tbl_bookOrders;
-------------------------------------------------------------------------------------------------------
create table tbl_bookKartList( bookId int not null,orderQty int not null,cartId int ,
							constraint pk_bookKartList primary key(bookId,cartId),
							constraint fk_bookId foreign key (bookId) references tbl_books,
							constraint chk_orderQty check (orderQty > 0),
							constraint fk_listcartId foreign key(cartId) references tbl_shoppingCart);

insert into tbl_bookKartList values(1, 2, 1);
insert into tbl_bookKartList values(2, 1, 1);
insert into tbl_bookKartList values(5, 2, 1);

select * from tbl_bookKartList;
-------------------------------------------------------------------------------------------------------------------
----------------------------------------------WishList Table--------------------------------------------------------

create table tbl_wishlist(userName varchar(20), bookId int,
						  constraint pk_userNamebookId primary key (userName, bookId));

---------------------------------------------------------------------------------------------------------------------
-- Ordering
					
-- Book store but there is no count for how many books are present in the store
-- How do we check the status of the book

	create procedure sp_createOrder(@cartId int, @couponName varchar(20) = NULL)
	as begin
		declare @orderSum float;
		declare @couponId int;
		set @couponId = (select couponId from tbl_coupons where couponName = @couponName);
		set @orderSum = (select SUM(bookPrice*orderQty) from (tbl_books  join tbl_bookKartList  on tbl_books.bookId=tbl_bookKartList.bookId)join tbl_shoppingCart on tbl_bookKartList.cartId=tbl_shoppingCart.cartId where tbl_bookKartList.cartId=@cartId);
		if(@couponId IS NOT NULL)
		begin
			set @orderSum = (@orderSum - ((select couponDiscount from tbl_coupons where couponId = @couponId) * @orderSum));
		end
		insert into tbl_bookOrders values(@cartId, @couponId, @orderSum);
		update tbl_shoppingCart set orderStatus = 1 where cartId = @cartId;
	end

	exec sp_createOrder 1, '20% Discount';

	select * from tbl_bookOrders;

	drop procedure sp_createOrder;
------------------------------------------------------------------------------------------------------------------------------------
-- 1. Admin shall be able to login or logout ------------------------------------------------------

-- Best method would be to get the password hash from the database for a username
--	and then compare the hash with the hash of the user input
-- https://stackoverflow.com/questions/46819734/how-to-check-username-and-password-matches-the-database-values

--create procedure sp_checkAdminPassword(@userName varchar(20), @passWord varchar(20))
--as
--begin
--	declare @hashedPassword binary(32), @storedPassword binary(32);

--	set @hashedPassword = HASHBYTES('SHA2_256', @passWord);
--	set @storedPassword = (select adminPassword from tbl_adminCredentials where adminUserName = @userName);

--	if(@hashedPassword = @storedPassword)
--	begin
--		print 'Logged In';
--		return 1;
--	end

--	else
--	begin
--		print 'Invalid Credentials';
--		return 0;
--	end

--end

drop procedure sp_checkAdminPassword;



create procedure sp_checkAdminPassword(@isValidCredentials bit output, @userName varchar(20), @passWord varchar(20))
as
begin
	declare @hashedPassword binary(32), @storedPassword binary(32);

	set @hashedPassword = HASHBYTES('SHA2_256', @passWord);
	set @storedPassword = (select adminPassword from tbl_adminCredentials where adminUserName = @userName);

	if(@hashedPassword = @storedPassword)
	begin
		set @isValidCredentials = 1;
	end

	else
	begin
		set @isValidCredentials = 0;
	end

	return @isValidCredentials;
end

declare @outputs bit;
exec sp_checkAdminPassword @outputs out,'adminArjun', 'passwordArjun'
print @outputs
---------------------------------------------------------------------------------------------------

-- 2. There can be pre-defined username and password for admin-------------------------------------

-- storing the hashed password for security
-- using binary(32) because SHA2_256 outputs a 32 byte hash
-- https://docs.microsoft.com/en-us/sql/t-sql/functions/hashbytes-transact-sql?view=sql-server-ver15
-- https://security.stackexchange.com/questions/8596/https-security-should-password-be-hashed-server-side-or-client-side

create table tbl_adminCredentials(adminUserName varchar(20), adminPassword binary(32) not null,
								  constraint pk_adminUserName primary key (adminUserName));

drop table tbl_adminCredentials;

select HASHBYTES('SHA2_256', 'adminpassword');

insert into tbl_adminCredentials values('adminArjun', (select HASHBYTES('SHA2_256', 'passwordArjun')));
insert into tbl_adminCredentials values('adminJoel', (select HASHBYTES('SHA2_256', 'passwordJoel')));
insert into tbl_adminCredentials values('adminJohn', (select HASHBYTES('SHA2_256', 'passwordJohn')));

select * from tbl_adminCredentials;

---------------------------------------------------------------------------------------------------

-- 3. Admin shall be able to add / edit / delete category

drop table tbl_category;
select * from tbl_category;

-- Add category

create procedure sp_adminAddCategory(@categoryName varchar(20), @categoryDescription varchar(50), @categoryStatus bit, @categoryPosition varchar(20))
as
begin
	declare @date datetime;
	set @date = getdate();
	insert into tbl_category values(@categoryName, @categoryDescription, NULL, @categoryStatus, @categoryPosition, @date);
end

drop procedure sp_adminAddCategory;

exec sp_adminAddCategory 'Science Fiction1', 'Science Fictional Books', 1, '5'

select * from tbl_category;

-- Edit category
create procedure sp_adminUpdateCategory(@catId int,@categoryName varchar(20), @categoryDescription varchar(50), @categoryStatus bit, @categoryPosition varchar(20))
as begin
	update tbl_category set categoryName = @categoryName, categoryDescription = @categoryDescription, categoryStatus = @categoryStatus, categoryPosition = @categoryPosition 
	where categoryId = @catId
end
exec sp_adminUpdateCategory 8,'Science Fiction2','Science Fictional Books1',0,7
--	Delete Category
create procedure sp_adminDeleteCategory(@catId int)
as begin
	delete from tbl_category where categoryId = @catId
end


---------------------------------------------------------------------------------------------------

-- 4. Category must be sorted by position

select * from tbl_category order by categoryPosition asc;

---------------------------------------------------------------------------------------------------

-- 5. Display category only where status is true

create view trueStatusCategories
as
	select * from tbl_category where categoryStatus = 1;

select * from trueStatusCategories;

drop procedure sp_searchByCategory;

---------------------------------------------------------------------------------------------------

-- 6. Display Category List on Home Page

select categoryName from tbl_category;

---------------------------------------------------------------------------------------------------------------
-----------------------------Admin:Add Books----------------------------------------------------------------------


alter procedure sp_adminAddBooks(@catid int,@bookTitle varchar(20),@bookISBN varchar(17),@bookAuthor varchar(20),@bookYear smallint,@bookPrice float,
									@bookDescription varchar(50), @bookPosition varchar(20), 
									@bookStatus bit)
as begin
	insert into tbl_books values(@catid,@bookTitle,@bookISBN,@bookAuthor,@bookYear,@bookPrice,@bookDescription,@bookPosition,@bookStatus,NULL)
end
exec sp_adminAddBooks 2,'Iron Man 1','1236567890987','Stan Lee',1959,200,'Iron Man Comic','5',1

select * from tbl_books

-----------------------------Admin:Delete Books----------------------------------------------------------------------
create procedure sp_adminDeleteBook(@bookId int)
as begin
	delete from tbl_books where bookId = @bookId
end
exec sp_adminDeleteBook 13
---------------------------------------------------------------------------------------------------------------------
----------------------------Admin:Procedure to Search Book by category-----------------------------------
create procedure sp_adminSearchBookByCategory(@catName varchar(20))
as begin
	select * from tbl_books where categoryId = (select categoryId from tbl_category where categoryName = @catName)
end

exec sp_adminSearchBookByCategory 'Fiction';

--------------------------------------------------------------------------------------------------------------------
--------------------------------------Admin:Activate,Deactivate User------------------------------------------------------
create procedure sp_adminActivateUser(@userName varchar(20))
as begin
	update tbl_userDetails set userStatus = 1 where userName = @userName
end

create procedure sp_adminDeactivateUser(@userName varchar(20))
as begin
	update tbl_userDetails set userStatus = 0 where userName = @userName
end

exec sp_adminDeactivateUser 'John'
select * from tbl_userDetails;
exec sp_adminActivateUser 'John'
-------------------------------------------------------------------------------------------------------------------------------------
------------------------------------Admin:View Orders------------------------------------------------------------------------------
	create procedure sp_adminViewUserOrder(@userName varchar(20))
	as begin
		
	end
--------------------------------------------------------------------------------------------------------------------------------------
----------------------------Admin:Add Cupon-------------------------------------------------------------------------------------------
	create procedure sp_adminAddCoupon(@couponName varchar(20) , @discountRate float)
	as begin
		insert into tbl_coupons values(@couponName,@discountRate);
	end

	exec sp_adminAddCoupon '20% Discount', .2

	select * from tbl_coupons;
------------------------------------------------------------------------------------------------------------------------------------------------

-----------------------------User Registration------------------------------------------------------------------------------------------------

create procedure sp_userRegister(@userName varchar(20), @userPassword varchar(20) , @shippingAddress varchar(50))
as begin
	insert into tbl_userDetails values(@userName,@userPassword,1,@shippingAddress);
end

exec sp_userRegister 'Joel', '123456', 'address 1'
exec sp_userRegister 'Arun', '123456', 'address 1'

select * from tbl_userDetails;
---------------------------------------------------------------------------------------------------------------------------------------------------------	
	
-------------------------------------------------Search Books by Name---------------------------------------------------------------------------------------------	
	create procedure sp_searchByName(@Name varchar(20))
	as begin
	select * from tbl_books where bookTitle=@name;
	end

	drop procedure sp_searchByName;

	exec sp_searchByName 'Witcher';

	select * from tbl_books;
----------------------------------------------------------------------------------------------------------------------------------------------------------------	
---------------------------------------------------------------Search Books By Category------------------------------------------------------------------------
	create procedure sp_searchByCategory(@Category varchar(20))
	as begin
	select * from tbl_books join tbl_category on tbl_books.categoryId=tbl_category.categoryId where categoryName=@category;
	end

	exec sp_searchByCategory 'Comics'
----------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------Search Books By ISBN------------------------------------------------------------------------
	create procedure sp_searchByISBN(@ISBN varchar(17))
	as begin
	select * from tbl_books where bookISBN=@ISBN;
	end

	exec sp_searchByISBN 9780747532743
---------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------Search Books By Author------------------------------------------------------------------------

	create procedure sp_searchByauthor(@author varchar(20))
	as begin
	select * from tbl_books where bookAuthor=@author;
	end

	exec sp_searchByauthor 'Hideo Kojima'
---------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------4. only show true status books-------------------------------------------------------------------------------------------------------
	
	create procedure sp_activeBooks
	as begin
	select * from tbl_books where bookStatus=1
	end
	
	exec sp_activeBooks 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------5 see new books---------------------------------------------------------------------------------------------------------
	create procedure sp_newBooks
	as begin
	select * from tbl_books order by bookYear desc
	end

	exec sp_newBooks
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
--------------------------------------------------------------6 featured book------------------------------------------------------------------------------------------------------------
	create procedure sp_featuredBook
	as begin
	select * from tbl_books where bookTitle = (select top 1 bookTitle from tbl_books order by bookPosition asc);
	end

	drop procedure sp_featuredBook;

	exec sp_featuredBook
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------7 wishlist operations of his or hr own book------------------------------------------------------------------------------------------------------------
	create procedure sp_addToWishlist(@userName varchar(20), @bookTitle varchar(20))
	as
	begin
		insert into tbl_wishlist values(@userName, (select bookId from tbl_books where bookTitle = @bookTitle));
	end

	exec sp_addToWishlist 'John', 'Witcher'

	create procedure sp_removeFromWishlist(@userName varchar(20), @bookTitle varchar(20))
	as
	begin
		delete from tbl_wishlist  where userName = @userName and bookId =  (select bookId from tbl_books where bookTitle = @bookTitle);
	end

	exec sp_removeFromWishlist 'John', 'Harry Potter'

	select * from tbl_wishlist;
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	

    --8 add books ----------------------------------- TODO ---------------------------------------------------
	create procedure sp_addBooktoKart(@bookId int,@orderQty int,@cartId int)
	as begin
		insert into tbl_bookKartList values(@bookId,@orderQty,@cartId)
	end
	
	exec sp_addBooktoKart 3, 1, 1
	
	select * from tbl_bookKartList
	---------------------------------------------------------------------------------------------------------------

	--9 user must be login before placing order
	-- not on db. inside api

	--10 manage cart

	select * from tbl_shoppingCart;
	select * from tbl_bookKartList;

	--delete book
	create procedure sp_deleteFromCart(@bookName varchar(20), @userName varchar(20))
	as
	begin
		declare @bookId int;
		declare @cartId int;

		set @bookId = (select bookId from tbl_books where bookTitle = @bookName);
		set @cartId = (select cartId from tbl_shoppingCart where userName = @userName and orderStatus = 0);

		delete from tbl_bookKartList where bookId = @bookId and cartId = @cartId;
	end

	exec sp_deleteFromCart 'Harry Potter', 'Arun'

	select * from tbl_bookKartList;
	select * from tbl_books;

	
	--update qty
	alter procedure sp_subQuantity( @bookName varchar(20), @orderQty int, @userName varchar(20))
	as
	begin
		declare @cartId int;
		declare @bookId int;
		declare @bookCount int;

		set @bookId = (select bookId from tbl_books where bookTitle = @bookName);
		set @cartId = (select cartId from tbl_shoppingCart where userName = @userName and orderStatus = 0);
		set @bookCount = (select orderQty from tbl_bookKartList where cartId = @cartId and bookId = @bookId) - @orderQty;

		if(@bookCount > 0)
		begin
			update tbl_bookKartList set orderQty=@bookCount where bookId =@bookId and cartId =  @cartId 
		end
		else
		begin
			exec sp_deleteFromCart @bookName, @userName;
		end
	end

	create procedure sp_addQuantity( @bookName varchar(20), @orderQty int, @userName varchar(20))
	as
	begin
		declare @cartId int;
		declare @bookId int;

		set @bookId = (select bookId from tbl_books where bookTitle = @bookName);
		set @cartId = (select cartId from tbl_shoppingCart where userName = @userName and orderStatus = 0);

		update tbl_bookKartList set orderQty=orderQty + @orderQty where bookId =@bookId and cartId =  @cartId 
	end

	exec sp_addQuantity 'Sherlock Holmes', 10, 'Arun';

	exec sp_subQuantity @output out, 'Witcher', 1, 'Arun';

	--11 save edit delte shipping adress

	select * from tbl_userDetails

	create procedure sp_updateShippingAddress(@userName varchar(20), @newShippingAddress varchar(50) = NULL)
	as
	begin
		update tbl_userDetails set shippingAddress=@newShippingAddress where userName = @userName 
	end

	exec sp_updateShippingAddress 'Arjun', 'Address1233'

	--12 

alter procedure onLogin(@outputstring varchar(20) output,@userName varchar(20),@password varchar(20))
as begin 
declare @tuser varchar(20)
declare @tpass varchar(20)
declare @ActiveCart int
set @tuser = (select userName from tbl_userDetails where userName = @userName )
set @tpass = (select userPassword from tbl_userDetails where userName =@userName)
	if (@userName = @tuser and @password = @tpass )
	begin
		set @outputstring = @userName 
		set @ActiveCart = (select count(*) from tbl_shoppingCart where userName=@userName and orderStatus=0 )
		if(@ActiveCart = 0)
		begin
			insert into tbl_shoppingCart values (@userName,0)
		end
	end
	if(@userName = @tuser and @password != @tpass)
	begin
		set @outputstring = 'Incorrect credentials' 
	end
--	if(@tuser = '')
--	begin
--		set @outputstring = 'Incorrect credentials' 
--	end
end

select userName from tbl_userDetails where userName = 'Aasdf';

declare @outputs varchar(30);
exec onLogin @outputs output, 'Arun', '123456';
print @outputs

select * from tbl_shoppingCart;

update tbl_shoppingCart set orderStatus = 1 where cartId = 2;
------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------- Selecting an image from a table-----------------------------------------------------------

--CREATE TABLE dbo.Images

--(

--      [ImageID] [int] IDENTITY(1,1) NOT NULL,

--      [ImageName] [varchar](40) NOT NULL,

--      [OriginalFormat] [nvarchar](5) NOT NULL, 

--      [ImageFile] [varbinary](max) NOT NULL

-- )  

 

--    INSERT INTO dbo.Images

--    (

--           ImageName

--          ,OriginalFormat

--          ,ImageFile

--    )

--    SELECT

--          'Sample Image'

--          ,'png'

--          ,ImageFile

--    FROM OPENROWSET(BULK N'C:\Users\jkuruvila\Desktop\dog.png', SINGLE_BLOB) AS ImageSource(ImageFile);

--	select * from dbo.adminAs;

	-----------------------------------------------------------------------


--- Log day 1
	-- Created all the tables
	-- TODO - Shopping cart and orders table need to be normalized
	-- TODO - create triggers to calculate totalCost
	-- TODO - crate prodecures for viewing data by admins and users