import React, { Component } from "react";
import PlaninarDataService from "../../services/planinar.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";


export default class DodajPlaninar extends Component {

  constructor(props) {
    super(props);
    this.dodajPlaninar = this.dodajPlaninar.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  async dodajPlaninar(planinar) {
    const odgovor = await PlaninarDataService.post(planinar);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/planinar';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }



  handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);

    this.dodajPlaninar({
      ime: podaci.get('ime'),
      prezime: podaci.get('prezime'),
      oib: podaci.get('oib'),
      pldrustvo: podaci.get('pldrustvo')
    });
    
  }


  render() { 
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Josip" maxLength={255} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" placeholder="Horvat" required />
          </Form.Group>

          <Form.Group className="mb-3" controlId="oib">
            <Form.Label>OIB</Form.Label>
            <Form.Control type="text" name="oib" placeholder="" />
          </Form.Group>

          <Form.Group className="mb-3" controlId="pldrustvo">
            <Form.Label>PlDrustvo</Form.Label>
            <Form.Control type="text" name="pldrustvo" placeholder="PD Đakovo" />
          </Form.Group>

          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/planinar`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj planinara
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

