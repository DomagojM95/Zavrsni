import React, { Component } from "react";
import PlaninarDataService from "../../services/planinar.services";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";
import { FaEdit } from 'react-icons/fa';
import { FaTrash } from 'react-icons/fa';
import { Modal } from 'react-bootstrap';


export default class Planinari extends Component {
  constructor(props) {
    super(props);
    this.dohvatiPlaninari = this.dohvatiPlaninari.bind(this);

    this.state = {
      planinari: [],
      prikaziModal: false
    };
  }



  otvoriModal = () => this.setState({ prikaziModal: true });
  zatvoriModal = () => this.setState({ prikaziModal: false });

  componentDidMount() {
    this.dohvatiPlaninari();
  }
  dohvatiPlaninari() {
    PlaninarDataService.getAll()
      .then(response => {
        this.setState({
          planinari: response.data
        });
      })
      .catch(e => {
        console.log(e);
      });
  }

  async obrisiPlaninar(sifra){
    
    const odgovor = await PlaninarDataService.delete(sifra);
    if(odgovor.ok){
     this.dohvatiPlaninari();
    }else{
     // alert(odgovor.poruka);
      this.otvoriModal();
    }
    
   }

  render() {
    const { planinari} = this.state;
    return (

    <Container>
      <a href="/planinar/dodaj" className="btn btn-success gumb">Dodaj novog planinara</a>
    <Row>
      { planinari && planinari.map((p) => (
           
           <Col key={p.sifra} sm={12} lg={3} md={3}>

              <Card style={{ width: '18rem' }}>
                <Card.Body>
                  <Card.Title>{p.ime} {p.prezime}</Card.Title>
                  <Card.Text>
                   Oib:  {p.oib} 
                    
                  </Card.Text>
                  <Card.Text>
                    {p.pldrustvo}
                    
                  </Card.Text>

                  <Row>
                      <Col>
                      <Link className="btn btn-primary gumb" to={`/planinari/${p.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                      <Button variant="danger" className="gumb"  onClick={() => this.obrisiPlaninar(p.sifra)}><FaTrash /></Button>
                      </Col>
                    </Row>
                </Card.Body>
              </Card>
            </Col>
          ))
      }
      </Row>


      <Modal show={this.state.prikaziModal} onHide={this.zatvoriModal}>
              <Modal.Header closeButton>
                <Modal.Title>Greška prilikom brisanja</Modal.Title>
              </Modal.Header>
              <Modal.Body>Planinar se nalazi na jednom ili više dnevnika i ne može se obrisati.</Modal.Body>
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
