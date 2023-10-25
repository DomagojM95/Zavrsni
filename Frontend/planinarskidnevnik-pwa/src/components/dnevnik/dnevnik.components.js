import React, { Component } from "react";
import DnevnikDataService from "../../services/dnevnik.services";
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


export default class Dnevnici extends Component {
  constructor(props) {
    super(props);

    
    this.dohvatiDnevnike = this.dohvatiDnevnike.bind(this);

    this.state = {
      dnevnici: [],
      prikaziModal: false
    };
  }

  otvoriModal = () => this.setState({ prikaziModal: true });
  zatvoriModal = () => this.setState({ prikaziModal: false });


  componentDidMount() {
    this.dohvatiDnevnike();
  }
  dohvatiDnevnike() {
    DnevnikDataService.getAll()
      .then(response => {
        this.setState({
          dnevnici: response.data
        });
        console.log(response);
      })
      .catch(e => {
        console.log(e);
      });
  }

  async obrisiDnevnik(sifra){
    
    const odgovor = await DnevnikDataService.delete(sifra);
    if(odgovor.ok){
     this.dohvatiDnevnike();
    }else{
     this.otvoriModal();
    }
    
   }

  render() {
    const { dnevnici} = this.state;
    return (

    <Container>
      <a href="/dnevnik/dodaj" className="btn btn-success gumb">Dodaj novi dnevnik</a>
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
              {dnevnici && dnevnici.map((d,index) => (
                
                <tr key={index}>
                  <td> 
                    <p className="naslovDnevnik">{d.naziv} </p>
                  </td>

                  <td> 
                   
                    {d.izlet}
                  </td>

                  <td> 
                   
                    {d.planinar}
                  </td>

                  <td>
                    <Row>
                      <Col>
                        <Link className="btn btn-primary gumb" to={`/dnevnik/${d.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                        { 
                             <Button variant="danger"  className="gumb" onClick={() => this.obrisiDnevnik(d.sifra)}><FaTrash /></Button>
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