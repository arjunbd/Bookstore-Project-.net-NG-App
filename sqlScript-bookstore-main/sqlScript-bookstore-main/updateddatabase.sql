USE master

/****** Object:  Database [bookstoreDB2]    Script Date: 28-10-2021 19:56:32 ******/
CREATE DATABASE bookstoreDB2

USE bookstoreDB2

/****** Object:  Table [dbo].[tbl_adminCredentials]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_adminCredentials(
	adminUserName varchar(20) NOT NULL,
	adminPassword binary(32) NOT NULL,
 CONSTRAINT pk_adminUserName PRIMARY KEY (adminUserName))
	
/****** Object:  Table [dbo].[tbl_bookKartList]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_bookKartList(
	bookId int NOT NULL,
	orderQty int NOT NULL,
	cartId int NOT NULL,
 CONSTRAINT pk_bookKartList PRIMARY KEY(bookId,cartId)) 
/****** Object:  Table [dbo].[tbl_bookOrders]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_bookOrders(
	orderId int IDENTITY(1,1) NOT NULL,
	cartId int NULL,
	couponId int NULL,
	totalCost float NOT NULL,
 CONSTRAINT pk_orderId PRIMARY KEY(orderId)) 
/****** Object:  Table [dbo].[tbl_books]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_books(
	bookId int IDENTITY(1,1) NOT NULL,
	categoryId int NULL,
	bookTitle varchar(20) NOT NULL,
	bookISBN varchar(17) NOT NULL,
	bookYear smallint NULL,
	bookPrice float NOT NULL,
	bookDescription varchar(50) NULL,
	bookPosition varchar(20) NOT NULL,
	bookStatus bit NOT NULL,
	bookImage varchar(50) NULL,
	bookAuthor varchar(20) NULL,
 CONSTRAINT pk_bookId PRIMARY KEY (bookId))
/****** Object:  Table [dbo].[tbl_category]    Script Date: 28-10-2021 19:56:32 ******/

create TABLE tbl_category(
	categoryId [int] IDENTITY(1,1) NOT NULL,
	categoryName [varchar](20) NOT NULL,
	categoryDescription [varchar](50) NULL,
	categoryImage [varchar](50) NULL,
	categoryStatus [bit] NOT NULL,
	categoryPosition [varchar](20) NOT NULL,
	categoryCreatedAt [datetime] NULL,
 CONSTRAINT [pk_categoryId] PRIMARY KEY (categoryId),

 CONSTRAINT [uk_categoryName] UNIQUE (categoryName) ,

 CONSTRAINT [uk_categoryPosition] UNIQUE (categoryPosition))

/****** Object:  Table [dbo].[tbl_coupons]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_coupons(
	[couponId] [int] IDENTITY(1,1) NOT NULL,
	[couponName] [varchar](20) NOT NULL,
	[couponDiscount] [float] NOT NULL,
 CONSTRAINT [pk_couponId] PRIMARY KEY (couponId))

/****** Object:  Table [dbo].[tbl_shoppingCart]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_shoppingCart(
	[cartId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](20) NOT NULL,
	[orderStatus] [bit] NULL,
 CONSTRAINT [pk_cartId] PRIMARY KEY (cartId))
/****** Object:  Table [dbo].[tbl_userDetails]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_userDetails(
	[userName] [varchar](20) NOT NULL,
	[userPassword] [varchar](20) NOT NULL,
	[userStatus] [bit] NOT NULL,
	[shippingAddress] [varchar](50) NULL,
 CONSTRAINT [pk_userName] PRIMARY KEY (userName))
/****** Object:  Table [dbo].[tbl_wishlist]    Script Date: 28-10-2021 19:56:32 ******/

CREATE TABLE tbl_wishlist(
	[userName] [varchar](20) NOT NULL,
	[bookId] [int] NOT NULL,
 CONSTRAINT [pk_userNamebookId] PRIMARY KEY(userName,bookId) )
ALTER TABLE [dbo].[tbl_shoppingCart] ADD  CONSTRAINT [df_orderStatus]  DEFAULT ((0)) FOR [orderStatus]
GO
ALTER TABLE [dbo].[tbl_bookKartList]  WITH CHECK ADD  CONSTRAINT [fk_bookId] FOREIGN KEY([bookId])
REFERENCES [dbo].[tbl_books] ([bookId])
GO
ALTER TABLE [dbo].[tbl_bookKartList] CHECK CONSTRAINT [fk_bookId]
GO
ALTER TABLE [dbo].[tbl_bookKartList]  WITH CHECK ADD  CONSTRAINT [fk_listcartId] FOREIGN KEY([cartId])
REFERENCES [dbo].[tbl_shoppingCart] ([cartId])
GO
ALTER TABLE [dbo].[tbl_bookKartList] CHECK CONSTRAINT [fk_listcartId]
GO
ALTER TABLE [dbo].[tbl_bookOrders]  WITH CHECK ADD  CONSTRAINT [fk_cartId] FOREIGN KEY([cartId])
REFERENCES [dbo].[tbl_shoppingCart] ([cartId])
GO
ALTER TABLE [dbo].[tbl_bookOrders] CHECK CONSTRAINT [fk_cartId]
GO
ALTER TABLE [dbo].[tbl_bookOrders]  WITH CHECK ADD  CONSTRAINT [fk_couponId] FOREIGN KEY([couponId])
REFERENCES [dbo].[tbl_coupons] ([couponId])
GO
ALTER TABLE [dbo].[tbl_bookOrders] CHECK CONSTRAINT [fk_couponId]
GO
ALTER TABLE [dbo].[tbl_books]  WITH CHECK ADD  CONSTRAINT [fk_categoryId] FOREIGN KEY([categoryId])
REFERENCES [dbo].[tbl_category] ([categoryId])
GO
ALTER TABLE [dbo].[tbl_books] CHECK CONSTRAINT [fk_categoryId]
GO
ALTER TABLE [dbo].[tbl_shoppingCart]  WITH CHECK ADD  CONSTRAINT [fk_userName] FOREIGN KEY([userName])
REFERENCES [dbo].[tbl_userDetails] ([userName])
GO
ALTER TABLE [dbo].[tbl_shoppingCart] CHECK CONSTRAINT [fk_userName]
GO
ALTER TABLE [dbo].[tbl_bookKartList]  WITH CHECK ADD  CONSTRAINT [chk_orderQty] CHECK  (([orderQty]>(0)))
GO
ALTER TABLE [dbo].[tbl_bookKartList] CHECK CONSTRAINT [chk_orderQty]
GO
/****** Object:  StoredProcedure [dbo].[onLogin]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[onLogin](@outputstring varchar(20) output,@userName varchar(20),@password varchar(20))
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
GO
/****** Object:  StoredProcedure [dbo].[SeeCart]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[SeeCart](@cartId int)
as begin
select * from tbl_bookKartList where cartId=@cartId
end
GO
/****** Object:  StoredProcedure [dbo].[sp_activeBooks]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[sp_activeBooks]
	as begin
	select * from tbl_books where bookStatus=1
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_addBooktoKart]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[sp_addBooktoKart](@bookId int,@orderQty int,@cartId int)
	as begin
		insert into tbl_bookKartList values(@bookId,@orderQty,@cartId)
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_addCategory]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[sp_addCategory](@categoryName varchar(20), @categoryDescription varchar(50), @categoryImage varbinary(max), @categoryStatus bit, @categoryPosition varchar(20))
as
begin
	declare @date datetime;
	set @date = getdate();
	insert into tbl_category values(@categoryName, @categoryDescription, @categoryImage, @categoryStatus, @categoryPosition, @date);
end
GO
/****** Object:  StoredProcedure [dbo].[sp_addQuantity]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[sp_addQuantity]( @bookName varchar(20), @orderQty int, @userName varchar(20))
	as
	begin
		declare @cartId int;
		declare @bookId int;

		set @bookId = (select bookId from tbl_books where bookTitle = @bookName);
		set @cartId = (select cartId from tbl_shoppingCart where userName = @userName and orderStatus = 0);

		update tbl_bookKartList set orderQty=orderQty + @orderQty where bookId =@bookId and cartId =  @cartId 
	end

GO
/****** Object:  StoredProcedure [dbo].[sp_addToWishlist]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[sp_addToWishlist](@userName varchar(20), @bookTitle varchar(20))
	as
	begin
		insert into tbl_wishlist values(@userName, (select bookId from tbl_books where bookTitle = @bookTitle));
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_adminActivateUser]    Script Date: 28-10-2021 19:56:32 ******/

create procedure [dbo].[sp_adminActivateUser](@userName varchar(20))
	as begin
		update tbl_userDetails set userStatus = 1 where userName = @userName
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_adminAddCupon]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_adminAddCupon](@cuponName varchar(20) , @discountRate float)
	as begin
		insert into tbl_coupons values(@cuponName,@discountRate);
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_adminDeactivateUser]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_adminDeactivateUser](@userName varchar(20))
	as begin
		update tbl_userDetails set userStatus = 0 where userName = @userName
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_adminSearchBookByCategory]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_adminSearchBookByCategory](@catName varchar(20))
	as begin
		select * from tbl_books where categoryId = (select categoryId from tbl_category where categoryName = @catName)
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_checkAdminPassword]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_checkAdminPassword](@userName varchar(20), @passWord varchar(20))
as
begin
	declare @hashedPassword binary(32), @storedPassword binary(32);

	set @hashedPassword = HASHBYTES('SHA2_256', @passWord);
	set @storedPassword = (select adminPassword from tbl_adminCredentials where adminUserName = @userName);

	if(@hashedPassword = @storedPassword)
	begin
		print 'Logged In';
		return 1;
	end

	else
	begin
		print 'Invalid Credentials';
		return 0;
	end

end
GO
/****** Object:  StoredProcedure [dbo].[sp_createOrder]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	create procedure [dbo].[sp_createOrder](@cartId int, @couponName varchar(20) = NULL)
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
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteFromCart]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_deleteFromCart](@bookName varchar(20), @userName varchar(20))
	as
	begin
		declare @bookId int;
		declare @cartId int;

		set @bookId = (select bookId from tbl_books where bookTitle = @bookName);
		set @cartId = (select cartId from tbl_shoppingCart where userName = @userName and orderStatus = 0);

		delete from tbl_bookKartList where bookId = @bookId and cartId = @cartId;
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_featuredBook]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_featuredBook]
	as begin
	select top 1 bookTitle from tbl_books order by bookPosition asc;
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_getCart]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_getCart](@cartId int)
as begin
select * from tbl_shoppingCart where cartId=@cartId
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getOrdersFromUsername]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_getOrdersFromUsername](@userName varchar(20))
as
begin
	select orderId, bookId, orderQty, totalCost from
	(select * from tbl_bookOrders where cartId in (select cartId from tbl_shoppingCart where userName = @userName)) as userOrders
	join tbl_bookKartList
	on userOrders.cartId = tbl_bookKartList.cartId
	order by orderId;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newBooks]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_newBooks]
	as begin
	select * from tbl_books order by bookYear desc
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_postCart]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_postCart](@userName varchar(20),@orderStatus bit)
as begin 
insert into tbl_shoppingCart values(@userName,@orderStatus)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_putshoppingcartstatus]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_putshoppingcartstatus](@username varchar(20), @orderStatus bit)
as begin
update tbl_shoppingCart set orderStatus=@orderStatus where userName=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_removeFromWishlist]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_removeFromWishlist](@userName varchar(20), @bookTitle varchar(20))
	as
	begin
		delete from tbl_wishlist  where userName = @userName and bookId =  (select bookId from tbl_books where bookTitle = @bookTitle);
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_searchByauthor]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_searchByauthor](@author varchar(20))
	as begin
	select * from tbl_books where bookAuthor=@author;
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_searchByCategory]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_searchByCategory](@Category varchar(20))
	as begin
	select * from tbl_books join tbl_category on tbl_books.categoryId=tbl_category.categoryId where categoryName=@category;
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_searchByISBN]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_searchByISBN](@ISBN varchar(17))
	as begin
	select * from tbl_books where bookISBN=@ISBN;
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_searchByName]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_searchByName](@Name varchar(20))
	as begin
	select * from tbl_books where bookTitle=@name;
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_subQuantity]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_subQuantity]( @bookName varchar(20), @orderQty int, @userName varchar(20))
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
GO
/****** Object:  StoredProcedure [dbo].[sp_updateShippingAddress]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_updateShippingAddress](@userName varchar(20), @newShippingAddress varchar(50) = NULL)
	as
	begin
		update tbl_userDetails set shippingAddress=@newShippingAddress where userName = @userName 
	end

GO
/****** Object:  StoredProcedure [dbo].[sp_userRegister]    Script Date: 28-10-2021 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_userRegister](@userName varchar(20), @userPassword varchar(20) , @shippingAddress varchar(50))
	as begin
		insert into tbl_userDetails values(@userName,@userPassword,1,@shippingAddress);
	end
GO
USE [master]
GO
ALTER DATABASE [bookstoreDB2] SET  READ_WRITE 
GO
/****** Object:  StoredProcedure [dbo].[sp_adminUpdateCategory]    Script Date: 28-10-2021 19:56:32 ******/
create procedure sp_adminUpdateCategory(@catId int,@categoryName varchar(20), @categoryDescription varchar(50), @categoryStatus bit, @categoryPosition varchar(20))as begin	update tbl_category set categoryName = @categoryName, categoryDescription = @categoryDescription, categoryStatus = @categoryStatus, categoryPosition = @categoryPosition 	where categoryId = @catIdend
