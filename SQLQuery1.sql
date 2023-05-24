use master;

drop database if exists pldnevnik;
go
 create database pldnevnik;

 go
  use pldnevnik;


  create table dnevnik(
	 sifra int  not null primary key identity,
	 naziv varchar(50)not null,
	 planinar int,
	 izlet int

  );

  create table planinar(
	sifra int  not null primary key identity,
	ime varchar(50) not null,
	prezime varchar(50) not null,
	oib int not null,
	pldrustvo varchar(50)not null,

  );

  create table izlet(
	sifra int  not null primary key identity,
	datum datetime not null,
	trajanje time,
	planina int not null,

  );

  create table planina(
	sifra int  not null primary key identity,
	ime varchar(50) not null,
	drzava varchar(50) not null,
	visina int

  );

  alter table dnevnik add foreign key(planinar) references planinar(sifra);
  alter table dnevnik add foreign key(izlet) references izlet(sifra); 
  alter table izlet add foreign key (planina) references planina(sifra);