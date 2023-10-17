import React, { Component } from "react";
import PlaninaDataService from "../../services/planina.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";


export default class DodajPlanina extends Component {

  constructor(props) {
    super(props);
    this.dodajPlanina = this.dodajPlanina.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  async dodajPlanina(izlet) {
    const odgovor = await PlaninaDataService.post(izlet);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/planine';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }



  handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);

    this.dodajPlanina({
        ime: podaci.get('ime'),
        drzava: podaci.get('drzava'),
        visina: podaci.get('visina'),
        
    });
    
  }


  render() { 
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Papuk" maxLength={255} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="drzava">
            <Form.Label>Drzava</Form.Label>
            <Form.Control type="text" name="drzava" placeholder="Hrvatska" required />
          </Form.Group>


          <Form.Group className="mb-3" controlId="visina">
            <Form.Label>Visina</Form.Label>
            <Form.Control type="number" name="visina" placeholder="89898" />
          </Form.Group>


          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/planine`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj planinu
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

