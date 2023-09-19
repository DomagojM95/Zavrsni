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
	oib varchar(50)not null,
	pldrustvo varchar(50)not null,

  );

  create table izlet(
	sifra int  not null primary key identity,
	naziv varchar(50)  not null,
	datum datetime not null,
	trajanje datetime,
	planina int not null,

  );

  create table planina(
	sifra int  not null primary key identity,
	ime varchar(50) not null,
	drzava varchar(50) not null,
	visina varchar(50)

  );

  alter table dnevnik add foreign key(planinar) references planinar(sifra);
  alter table dnevnik add foreign key(izlet) references izlet(sifra); 
  alter table izlet add foreign key (planina) references planina(sifra);

insert into planinar(ime,prezime,oib,pldrustvo)

values( 'Petar','Peric','00909009874','PD Dakovo'),
( 'Marko','Maric','07587987569','PD Zanatlija Osijek'),
( 'Ivo','Ivic','09485738294','PD Altius');

insert into planina(ime,drzava,visina)
values('Papuk','Hrvatska',950),
		('Velebit','Hrvatska',1757),
		('Dinara','Hrvatska',1831);

insert into izlet(datum,trajanje,planina,naziv)
values('2023-04-06','06:00:00',2,'izlet 1'),
		('2023-08-11','03:00:00',1,'izlet 2'),
		('2023-04-06','08:00:00',3,'izlet 3');
		
insert into dnevnik(naziv,planinar,izlet)
values('Dnevnik',3,1),
	  ('PL_Dnevnik',2,2),
	  ('Moj Pl Dnevnik',1,3);



	 

select d.ime
from planina a inner join izlet b on b.planina=a.sifra
inner join dnevnik c on c.izlet=b.sifra
inner join planinar d on c.planinar=d.sifra;


select d.ime, a.visina, a.ime
from planina a inner join izlet b on b.planina=a.sifra
inner join dnevnik c on c.izlet=b.sifra
inner join planinar d on c.planinar=d.sifra
where a.visina > 1000 order by 2;

select concat(d.ime, ' ', d.prezime) as planinar, a.visina, a.ime
from planina a inner join izlet b on b.planina=a.sifra
inner join dnevnik c on c.izlet=b.sifra
inner join planinar d on c.planinar=d.sifra
where a.visina > 1000 order by 2;