import React, { Component } from "react";
import IzletDataService from "../../services/izleti.services";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";
import { FaEdit } from 'react-icons/fa';
import { FaTrash } from 'react-icons/fa';
import moment from 'moment';
import { Modal } from 'react-bootstrap';


export default class Izleti extends Component {
  constructor(props) {
    super(props);

    
    this.dohvatiIzlete = this.dohvatiIzlete.bind(this);

    this.state = {
      izleti: [],
      prikaziModal: false
    };
  }

  otvoriModal = () => this.setState({ prikaziModal: true });
  zatvoriModal = () => this.setState({ prikaziModal: false });


  componentDidMount() {
    this.dohvatiIzlete();
  }
  dohvatiIzlete() {
    IzletDataService.getAll()
      .then(response => {
        this.setState({
          izleti: response.data
        });
        console.log(response);
      })
      .catch(e => {
        console.log(e);
      });
  }

  async obrisiIzlet(sifra){
    
    const odgovor = await IzletDataService.delete(sifra);
    if(odgovor.ok){
     this.dohvatiIzlete();
    }else{
     this.otvoriModal();
    }
    
   }

  render() {
    const { izleti} = this.state;
    return (

    <Container>
      <a href="/izlet/dodaj" className="btn btn-success gumb">Dodaj novi izlet</a>
      <Table striped bordered hover responsive>
              <thead>
                <tr>
                  <th>Naziv</th>
                  <th>Planina</th>
                  <th>Trajanje</th>
                  <th>Datum </th>
                  <th>Akcija</th>
                </tr>
              </thead>
              <tbody>
              {izleti && izleti.map((i,index) => (
                
                <tr key={index}>
                  <td> 
                    <p className="naslovPlanina">{i.naziv} </p>
                  </td>


                  <td> 
                   
                    {i.planina}
                  </td>

                  <td className="naslovPlanina">
                    {i.datum==null ? "Nije definirano" :
                    moment.utc(i.datum).format("HH")} h
                  </td>


                  <td className="naslovPlanina">
                    {i.datum==null ? "Nije definirano" :
                    moment.utc(i.datum).format("DD. MM. YYYY. ")}
                  </td>
                  <td>
                    <Row>
                      <Col>
                        <Link className="btn btn-primary gumb" to={`/izlet/${i.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                        { 
                             <Button variant="danger"  className="gumb" onClick={() => this.obrisiIzlet(i.sifra)}><FaTrash /></Button>
                        }
                      </Col>
                    </Row>
                    
                  </td>
                </tr>
                ))
              }
              </tbody>
            </Table>     

             <Modal show={this.state.prikaziModal} onHide={this.zatvoriModal}>
              <Modal.Header closeButton>
                <Modal.Title>Greška prilikom brisanja</Modal.Title>
              </Modal.Header>
              <Modal.Body>Izlet ima planine.</Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={this.zatvoriModal}>
                  Zatvori
                </Button>
              </Modal.Footer>
            </Modal>

    </Container>


    );
    
        }
}