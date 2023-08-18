import React, { useState, useEffect } from "react";
import client, { config } from '../utils';
import ImageCropDialog from "../ImageCropDialog";
import facebook from '../Images/facebook.svg'
import instagram from '../Images/instagram.svg'
import twitter from '../Images/twitter.svg'
import { Col, Row, Container } from "react-bootstrap";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';


const Medias = ({
    project
}) => {
    const [productsInProject, setProductsInProject] = useState([]);
    const [selectedProduct, setSelectedProduct] = useState();
    const [platform, setPlatform] = useState();
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const folderPath = '/projects/' + project + '/medias'
    useEffect(() => {

        // Fetch image links from your API endpoint    
        client.get(`DropBox/GetFolders/${encodeURIComponent(folderPath)}`, { headers: { 'Content-Type': 'application/json', } })
            .then(response => {
                setProductsInProject(response.data);
            })
            .catch(error => {
                console.error('Error fetching image links:', error);
            });
    }, []);
    const onProductSelect = (product) => {
        setSelectedProduct(product)
        handleShow()
    }
    const onPlatformSelect = (p) => {
        setPlatform(p)
    }
    console.log(selectedProduct)
    return (
        <>
            {selectedProduct && platform && (
                <ImageCropDialog
                    selectedPlatform={platform}
                    open={true}
                    handleClose={() => setSelectedProduct(null)}
                    product={selectedProduct}
                    project={project}
                />

            )}
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Choose Platform</Modal.Title>
                </Modal.Header>
                <Modal.Body> 
                    <div className="projectsContainer">
                        <Container>
                            <Row>
                                
                                <Col><img  onClick={() => {onPlatformSelect("Facebook");handleClose()}} xs={1} style={{ width: '40px' }} src={facebook}></img></Col>
                                <Col><img  onClick={() => onPlatformSelect("Instagram")} xs={1} style={{ width: '40px' }} src={instagram}></img></Col>
                                <Col><img  onClick={() => onPlatformSelect("Twitter")} xs={1} style={{ width: '40px' }} src={twitter}></img></Col>
                            </Row>
                            
                        </Container>

                </div></Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleClose}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
            <br></br>
            <div className="projectsContainer">
                <h2>Products in {project}</h2><br></br>
                <div class="folder-list">
                    {
                        productsInProject.map((name, index) => (
                            <>
                                <div class="folder">
                                    <span class="folder-icon">&#128193;</span>

                                    <span onClick={() => onProductSelect(name)} class="folder-name">{name}</span>
                                </div>
                            </>
                        ))
                    }
                </div>
            </div>  <br></br>
            
        </>
    )
}
export default Medias