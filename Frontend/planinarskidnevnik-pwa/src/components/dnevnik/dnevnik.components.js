import React, { Component } from "react";
import { Button, Container, Table } from "react-bootstrap";
import DnevnikDataService from "../../services/dnevnik.services";

import { Link } from "react-router-dom";
import {FaEdit, FaTrash} from "react-icons/fa"


export default class Dnevnici extends Component{

    constructor(props){
        super(props);

        this.state = {
            dnevnici: []
        };

    }

    componentDidMount(){
        this.dohvatiDnevnici();
    }

    async dohvatiDnevnici(){

        await DnevnikDataService.get()
        .then(response => {
            this.setState({
                dnevnici: response.data
            });
            console.log(response.data);
        })
        .catch(e =>{
            console.log(e);
        });
    }

    async obrisiDnevnik(sifra){
        const odgovor = await DnevnikDataService.delete(sifra);
        if(odgovor.ok){
            this.dohvatiDnevnik();
        }else{
            alert(odgovor.poruka);
        }
    }


    render(){

        const { dnevnici } = this.state;

        return (
            <Container>
               <a href="/dnevnici/dodaj" className="btn btn-success gumb">
                Dodaj novi dnevnik
               </a>
                
               <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Naziv</th>
                        <th>Izlet</th>
                        <th>Planinar</th>
                        <th>Akcija</th>
                    </tr>
                </thead>
                <tbody>
                   { dnevnici && dnevnici.map((dnevnik,index) => (

                    <tr key={index}>
                        <td>{dnevnik.naziv}</td>
                        <td>{dnevnik.izlet}</td>
                        <td>{dnevnik.planinar}</td>
                      
                        <td>
                            <Link className="btn btn-primary gumb"
                            to={`/dnevnici/${dnevnik.sifra}`}>
                                <FaEdit />
                            </Link>

                            <Button variant="danger" className="gumb"
                            onClick={()=>this.obrisiDnevnik(dnevnik.sifra)}>
                                <FaTrash />
                            </Button>
                        </td>
                    </tr>

                   ))}
                </tbody>
               </Table>



            </Container>


        );
    }
}