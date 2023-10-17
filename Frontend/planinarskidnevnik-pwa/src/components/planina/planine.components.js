import React, { Component } from "react";
import PlaninaDataService from "../../services/planina.services";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";
import { FaEdit } from 'react-icons/fa';
import { FaTrash } from 'react-icons/fa';
import { Modal } from 'react-bootstrap';


export default class Planine extends Component {
  constructor(props) {
    super(props);
    this.dohvatiPlanine = this.dohvatiPlanine.bind(this);

    this.state = {
      planine: [],
      prikaziModal: false
    };
  }



  otvoriModal = () => this.setState({ prikaziModal: true });
  zatvoriModal = () => this.setState({ prikaziModal: false });

  componentDidMount() {
    this.dohvatiPlanine();
  }
  dohvatiPlanine() {
    PlaninaDataService.getAll()
      .then(response => {
        this.setState({
          planine: response.data
        });
      })
      .catch(e => {
        console.log(e);
      });
  }

  async obrisiPlanina(sifra){
    
    const odgovor = await PlaninaDataService.delete(sifra);
    if(odgovor.ok){
     this.dohvatiPlanine();
    }else{
     // alert(odgovor.poruka);
      this.otvoriModal();
    }
    
   }

  render() {
    const { planine} = this.state;
    return (

    <Container>
      <a href="/planine/dodaj" className="btn btn-success gumb">Dodaj novu planinu</a>
    <Row>
      { planine && planine.map((p) => (
           
           <Col key={p.sifra} sm={12} lg={3} md={3}>

              <Card style={{ width: '18rem' }}>
                <Card.Body>
                  <Card.Title>{p.ime} {p.prezime}</Card.Title>
                  <Card.Text>
                    {p.email}
                  </Card.Text>
                  <Row>
                      <Col>
                      <Link className="btn btn-primary gumb" to={`/planine/${p.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                      <Button variant="danger" className="gumb"  onClick={() => this.obrisiPlanina(p.sifra)}><FaTrash /></Button>
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
              <Modal.Body>Planina se nalazi na jednoj ili više dnevnika i ne može se obrisati.</Modal.Body>
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
