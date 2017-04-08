-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

--BEGIN;

-- CREATE statements go here

--COMMIT;

create table recipe(
recipe_id INT IDENTITY (1,1) Primary Key,
recipe_name varchar(255) ,
recipe_type varchar(255),
image_name varchar(255),
recipe_description varchar(255),
cook_time int,
user_id int
);
alter table recipe add constraint fk_recipe_user foreign key (user_id) references users (user_id);

alter table recipe add user_id int;
alter table recipe add constraint fk_recipe_user foreign key (user_id) references users (user_id);

CREATE TABLE users(
user_id INT IDENTITY (1,1) Primary Key,
userName VARCHAR (256),
email VARCHAR(255) NOT NULL,
password varchar(32) NOT NULL
);

INSERT INTO users (userName, email, password) VALUES ('TestABC123', 'test@test.com', 'ABC123');
select * from users

create table recipe_ingredient(
recipe_id int,
ingredient_id int,
quantity int not null,

constraint pk_recipe_ingredient primary key (recipe_id, ingredient_id)
);

alter table recipe_ingredient add measurement varchar(255) not null;
alter table recipe_ingredient add constraint fk_rid_ritable foreign key (recipe_id) references recipe(recipe_id);
alter table recipe_ingredient add constraint fk_ingid_ritable foreign key (ingredient_id) references ingredient(ingredient_id);

create table preparation_steps(
step_id INT IDENTITY (1,1) ,
recipe_id int ,
steps varchar(255) not null,
constraint pk_preparation_steps primary key (step_id, recipe_id),
constraint fk_preparation_steps foreign key (recipe_id) references recipe(recipe_id)
);

select * from users

alter table users add salt varchar(64);
select * from users;
select * from recipe;

Insert INTO recipe VALUES('vegetable salad', 'vegetarian','vegetable salad','fresh garden vegetables health treat', '10', '2');

insert into recipe_ingredient values(1,1,1,'cup');
insert into recipe_ingredient values(1,2,1,'bunch');
insert into recipe_ingredient values(1,3,2,'cup');
insert into recipe_ingredient values(1,4,1,'bunch');

insert into preparation_steps values(1,'Mix fresh vegetables.');
insert into preparation_steps values(1,'Add Olive oil.');
insert into preparation_steps values(1,'Add italian dressing.');
insert into preparation_steps values(1,'add pinch of salt.');

Insert INTO recipe VALUES('Hard boiled egg', 'vegetarian','Hard boiled egg','proteinicious breakfast', '10', '2');

insert into recipe_ingredient values(2,70,100,'ml');
insert into recipe_ingredient values(2,955,3,'each');
insert into recipe_ingredient values(2,355,1,'tsp');
insert into recipe_ingredient values(2,516,1,'sprinkle');

insert into preparation_steps values(2,'add eggs in a container.');
insert into preparation_steps values(2,'Add water.');
insert into preparation_steps values(2,'Add salt and boil for 15 min.');
insert into preparation_steps values(2,'peal the shell and sprinkle pepper and enjoy.');