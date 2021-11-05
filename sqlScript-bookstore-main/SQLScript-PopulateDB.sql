
insert into tbl_adminCredentials values('admin', (select HASHBYTES('SHA2_256', 'adminpassword0')));
insert into tbl_adminCredentials values('admin1', (select HASHBYTES('SHA2_256', 'adminpassword1')));
insert into tbl_adminCredentials values('admin2', (select HASHBYTES('SHA2_256', 'adminpassword2')));
insert into tbl_adminCredentials values('admin3', (select HASHBYTES('SHA2_256', 'adminpassword3')));
insert into tbl_adminCredentials values('admin4', (select HASHBYTES('SHA2_256', 'adminpassword4')));
select * from tbl_adminCredentials
insert into tbl_userDetails values('user1','userpassword1',1,'useradress1');
insert into tbl_userDetails values('user2','userpassword2',1,'useradress2');
insert into tbl_userDetails values('user3','userpassword3',1,'useradress3');
insert into tbl_userDetails values('user4','userpassword4',1,'useradress4');
insert into tbl_userDetails values('user5','userpassword5',1,'useradress5');
select * from tbl_userDetails
insert into tbl_category values('Fiction','Fiction is any creative work in imaginary ways','update value with Image-path here',1,'1',getdate())
insert into tbl_category values('Horror','Horror seeks to elicit fear in its audience','update value with Image-path here',1,'2',getdate())
insert into tbl_category values('Fantasy','Fantasy is inspired by myth and folklore','update value with Image-path here',1,'3',getdate())
insert into tbl_category values('Comics','comics are presented through narrative art','update value with Image-path here',1,'4',getdate())
insert into tbl_category values('Action','Action includes spy novels and mysteries','update value with Image-path here',1,'5',getdate())
select * from tbl_category
insert into tbl_books values(3,'Harry Potter','9780747532743',1997,2600,'Story of a boy who discovers that he is a wizard','1',1,'put-image-path-here','J K Rowling')
insert into tbl_books values(3,'Percy Jackson','9780756966034',2005,5400,'Story of a boy who discovers that he is a demi-god','2',1,'put-image-path-here','Rick Riordan')
insert into tbl_books values(3,'Frankenstien','9780192822833',1818,100,'Story of a man who is brought back from death','3',1,'put-image-path-here','Mary Shelly')
insert into tbl_books values(3,'War of the worlds','9780866118705',1897,200,'Martian alien invasion on planet earth','4',1,'put-image-path-here','H G Wells')
insert into tbl_books values(3,'Dracula','9780192563930',1897,900,'Story of an undead count thirsting for blood','5',1,'put-image-path-here','Bram Stoker')
insert into tbl_books values(3,'1984','9780786181483',1949,700,'Story of a man suffering from social etiquettes','6',1,'put-image-path-here','George Orwell')
insert into tbl_books values(4,'IT','9780340951453',1986,2000,'Story of devil haunting a town','7',1,'put-image-path-here','Stephen King')
insert into tbl_books values(4,'The exorcist','9781504786010',1971,5400,'demonic pocession of a boy','8',1,'put-image-path-here','William peter')
insert into tbl_books values(4,'Women in black','9780582026605',1983,1000,'Story of a womans ghost haunting','9',1,'put-image-path-here','Susan Hill')
insert into tbl_books values(4,'Salems lot','9783785733059',1875,200,'Vampire imvasion in Maine','10',1,'put-image-path-here','Stephen King')
insert into tbl_books values(4,'Dracula','9780192563930',1897,900,'Story of an undead count thirsting for blood','11',1,'put-image-path-here','Bram Stoker')
insert into tbl_books values(4,'The Outsider','978076781010103',2018,2000,'Story of a man suffering from social etiquettes','12',1,'put-image-path-here','Stephen King')
select * from tbl_books
insert into tbl_coupons values('10% discount',0.1)
insert into tbl_coupons values('20% discount',0.2)
insert into tbl_coupons values('5% discount',0.05)
insert into tbl_coupons values('50% discount',0.5)
select * from tbl_coupons
insert into tbl_wishlist values('user1',2)
insert into tbl_wishlist values('user1',3)
insert into tbl_wishlist values('user1',4)
insert into tbl_wishlist values('user2',5)
insert into tbl_wishlist values('user2',6)
insert into tbl_wishlist values('user2',7)
select * from tbl_wishlist

select * from tbl_shoppingCart
select * from tbl_bookKartList
select * from tbl_bookOrders

declare @outputv varchar(30)
exec onLogin @outputv output , 'user1','userpassword1'
print @outputv
exec sp_addBooktoKart 5,3,1
exec sp_createOrder 1,'5% discount'