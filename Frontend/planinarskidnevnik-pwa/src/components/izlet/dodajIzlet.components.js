import React, { Component } from "react";
import IzletDataService from "../../services/izleti.services";
import PlaninaDataService from "../../services/planina.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";
import moment from 'moment';



export default class DodajIzlet extends Component {

  constructor(props) {
    super(props);
   
    this.dodajIzlet = this.dodajIzlet.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.dohvatiPlanine = this.dohvatiPlanine.bind(this);

    this.state = {
      planine: [],
      sifraPlanina:0
    };
  }

  componentDidMount() {
    //console.log("Dohvaćam smjerove");
    this.dohvatiPlanine();
  }

  async dodajIzlet(izlet) {
    const odgovor = await IzletDataService.post(izlet);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/izlet';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }


  async dohvatiPlanine() {

    await PlaninaDataService.get()
      .then(response => {
        this.setState({
          planine: response.data,
          sifraPlanina: response.data[0].sifra
        });

       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }


  handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);
   
    this.dodajIzlet({
      naziv: podaci.get('naziv'),
      datum: podaci.get('datum'),
      trajanje: podaci.get('trajanje'),
    
      sifraPlanina: this.state.sifraPlanina
    });
    
  }


  render() { 
    const {planine} = this.state;
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" placeholder="" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="planina">
            <Form.Label>Planina</Form.Label>
            <Form.Select onChange={e => {
              this.setState({ sifraPlanina: e.target.value});
            }}>
            {planine && planine.map((planina,index) => (
                  <option key={index} value={planina.sifra}>{planina.ime}</option>

            ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3" controlId="datum">
            <Form.Label>Datum </Form.Label>
            <Form.Control type="date" name="datum" placeholder=""  />
          </Form.Group>

          <Form.Group className="mb-3" controlId="trajanje">
            <Form.Label>Trajanje</Form.Label>
            <Form.Control type="date" name="trajanje" placeholder=""  />
          </Form.Group>

         



          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/izlet`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj izlet
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

