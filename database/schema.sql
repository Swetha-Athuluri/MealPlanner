-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

--BEGIN;

-- CREATE statements go here

--COMMIT;

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
  recipe_id int ,
  step_id INT IDENTITY (1,1) ,
  steps varchar(255) not null,
  constraint pk_preparation_steps primary key (step_id, recipe_id),
  constraint fk_preparation_steps foreign key (recipe_id)references recipe(recipe_id)
);