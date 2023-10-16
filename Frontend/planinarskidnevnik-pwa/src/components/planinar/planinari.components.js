import React, { Component } from "react";
import { Button, Container, Table } from "react-bootstrap";
import PlaninarDataService from "../../services/planinar.services";
import { NumericFormat } from "react-number-format";
import { Link } from "react-router-dom";
import {FaEdit, FaTrash} from "react-icons/fa"


export default class Planinari extends Component{

    constructor(props){
        super(props);
        this.dohvatiPlaninari = this.dohvatiPlaninari.bind(this);
        this.obrisiPlaninar = this.obrisiPlaninar.bind(this);

        this.state = {
            planinari: []
        };

    }

    componentDidMount(){
        this.dohvatiPlaninari();
    }

    async dohvatiPlaninari(){

        await PlaninarDataService.get()
        .then(response => {
            this.setState({
                planinari: response.data
            });
            console.log(response.data);
        })
        .catch(e =>{
            console.log(e);
        });
    }

    async obrisiPlaninar(sifra){
        const odgovor = await PlaninarDataService.delete(sifra);
        if(odgovor.ok){
            this.dohvatiPlaninari();
        }else{
            alert(odgovor.poruka);
        }
    }


    render(){

        const { planinari } = this.state;

        return (
            <Container>
               <a href="/planinari/dodaj" className="btn btn-success gumb">
                Dodaj novog Planinara
               </a>
                
               <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Ime</th>
                        <th>Prezime</th>
                        <th>PlDrustvo</th>
                        <th>Oib</th>
                        <th>Akcija</th>
                    </tr>
                </thead>
                <tbody>
                   { planinari && planinari.map((planinar,index) => (

                    <tr key={index}>
                        <td>{planinar.ime}</td>
                        <td>{planinar.prezime}</td>
                        <td>{planinar.PlDrustvo}</td>
                        <td>{planinar.oib}</td>
                      
                       
                        <td>
                            <Link className="btn btn-primary gumb"
                            to={`/planinari/${planinar.sifra}`}>
                                <FaEdit />
                            </Link>

                            <Button variant="danger" className="gumb"
                            onClick={()=>this.obrisiPlaninar(planinar.sifra)}>
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
