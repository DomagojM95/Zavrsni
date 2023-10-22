import React, { Component } from "react";
import { Button, Container, Table } from "react-bootstrap";
import IzletDataService from "../../services/izleti.services";
import { Link } from "react-router-dom";
import {FaEdit, FaTrash} from "react-icons/fa"


export default class Izleti extends Component{

    constructor(props){
        super(props);

        this.state = {
            izleti: []
        };

    }

    componentDidMount(){
        this.dohvatiIzleti();
    }

    async dohvatiIzleti(){

        await IzletDataService.get()
        .then(response => {
            this.setState({
                izleti: response.data
            });
            console.log(response.data);
        })
        .catch(e =>{
            console.log(e);
        });
    }

    async obrisiIzlet(sifra){
        const odgovor = await IzletDataService.delete(sifra);
        if(odgovor.ok){
            this.dohvatiIzleti();
        }else{
            alert(odgovor.poruka);
        }
    }


    render(){

        const { izleti } = this.state;

        return (
            <Container>
               <a href="/izleti/dodaj" className="btn btn-success gumb">
                Dodaj novi izlet
               </a>
                
               <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Naziv</th>
                        <th>Datum</th>
                        <th>Trajanje</th>
                        <th>Planina</th>
                       
                        <th>Akcija</th>
                    </tr>
                </thead>
                <tbody>
                   { izleti && izleti.map((izlet,index) => (

                    <tr key={index}>
                        <td>{izlet.naziv}</td>
                        <td>{izlet.datum}</td>
                        <td>{izlet.trajanje}</td>
                        <td>{izlet.planina}</td>
                        <td>
                            <Link className="btn btn-primary gumb"
                            to={`/izleti/${izlet.sifra}`}>
                                <FaEdit />
                            </Link>

                            <Button variant="danger" className="gumb"
                            onClick={()=>this.obrisiIzlet(izlet.sifra)}>
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