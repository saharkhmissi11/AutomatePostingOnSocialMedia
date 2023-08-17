import React, { useState, useEffect } from "react";
import Cropper from "react-easy-crop";
import getCroppedImg from "../CropImage";
import Button from 'react-bootstrap/Button';
import cropLogo from "../Images/crop-logo.png"
import zoomLogo from '../Images/zoom.png'
import facebook from '../Images/facebook.png'
import instagram from '../Images/instagram.png'
import twitter from '../Images/twitter.avif'
import Form from 'react-bootstrap/Form';
import "../App.css"
import "../ImageGallery/ImageGallery.css"
import Modal from 'react-bootstrap/Modal';
import { Col, Row, Container } from "react-bootstrap";
import client from "../utils";
import SideBar from "../SideBar/SideBar";
const aspectRatios = [
  { value: 4 / 5, text: "4/5" },
  { value: 16 / 9, text: "16/9" },
  { value: 1 / 2, text: "1/2" },
  { value: 1200 / 1400, text: "1200/1400" },
];


const Facebook = ({
  id,
  imageUrl,
  cropInit,
  zoomInit,
  aspectInit,
  onCancel,
  setCroppedImageFor,
  resetImage,
  open,
  handleClose
}) => {
  if (zoomInit == null) {
    zoomInit = 1;
  }
  if (cropInit == null) {
    cropInit = { x: 0, y: 0 };
  }
  if (aspectInit == null) {
    aspectInit = aspectRatios[0];
  }
  const VerticalLineWithShadow = () => {
    return (
      <div className="vertical-line"></div>
    );
  };
  const [zoom, setZoom] = useState(zoomInit);
  const [zoomI, setZoomI] = useState(zoomInit);
  const [zoomT, setZoomT] = useState(zoomInit);
  const [crop, setCrop] = useState(cropInit);
  const [cropI, setCropI] = useState(cropInit);
  const [cropT, setCropT] = useState(cropInit);
  const [croppedImageUrls, setCroppedImageUrls] = useState([]);
  const [aspect, setAspect] = useState(aspectInit);
  const [aspectI, setAspectI] = useState(aspectInit);
  const [aspectT, setAspectT] = useState(aspectInit);
  const [croppedAreaPixels, setCroppedAreaPixels] = useState(null);
  const [croppedAreaPixelsI, setCroppedAreaPixelsI] = useState(null);
  const [croppedAreaPixelsT, setCroppedAreaPixelsT] = useState(null);
  /*const [imagePathFacebook, setImagePathFacebook] = useState(null);
  const [imagePathInstagram, setImagePathInstagram] = useState(null);
  const [imagePathTwitter, setImagePathTwitter] = useState(null);*/
  const [imagePathFacebook, setImagePathFacebook] = useState(null);
  const [imagePathInstagram, setImagePathInstagram] = useState(null);
  const [imagePathTwitter, setImagePathTwitter] = useState(null);
  const [primaryProduct, setPrimaryProduct] = useState('');
  const [secondaryProducts, setSecondaryProducts] = useState('');
  const [imageLinks, setImageLinks] = useState([]);


  // Define min and max zoom levels
  const minZoom = 1;
  const maxZoom = 3;
  useEffect(() => {
    // Fetch image links from your API endpoint    
    client.get('DropBox/GetImagesLinks', { headers: { 'Content-Type': 'application/json', } })
      .then(response => {
        setImageLinks(response.data);
      })
      .catch(error => {
        console.error('Error fetching image links:', error);
      });
  }, []);
  const onCropChange = (crop) => {
    setCrop(crop);
  };


  const onZoomChange = (zoom) => {
    setZoom(zoom);
  };

  const onAspectChange = (e) => {
    const value = e.target.value;
    const ratio = aspectRatios.find((ratio) => ratio.value == value);
    setAspect(ratio);
  };

  const onCropComplete = (croppedArea, croppedAreaPixels) => {
    setCroppedAreaPixels(croppedAreaPixels);
  };


  const onCrop = async () => {
    const croppedImageUrl = await getCroppedImg(imageUrl, croppedAreaPixels, "Facebook");
    setImagePathFacebook(encodeURIComponent(croppedImageUrl));
    setCroppedImageFor(id, crop, zoom, aspect, croppedImageUrl);
    croppedImageUrls.push(croppedImageUrl);

  };




  const onResetImage = () => {
    resetImage(id);
  };
  // Secondary and Primary Products


  const handleButtonClick = () => {
    // Here, you can perform any additional processing before setting the state
    // For example, splitting secondary products by ';' and converting to an array
    const secondaryProductsArray = secondaryProducts.split(';').map(item => item.trim());

    // Update the state values
    setPrimaryProduct(primaryProduct);
    setSecondaryProducts(secondaryProductsArray.join('; ')); // Join back the array with '; ' separator
  };

  // encodeURIComponent(croppedImageUrl)
  const onBeginProcess = async () => {
    const p = "prim";
    const s = "secondary";
    handleButtonClick()
    const requestBody = {
      primaryProduct: primaryProduct,
      secondaryProducts: secondaryProducts,
      imagePathFacebook: await getCroppedImg(imageUrl, croppedAreaPixels, "Facebook"),
      imagePathInstagram: await getCroppedImg(imageUrl, croppedAreaPixelsI, "Instagram"),
      imagePathTwitter: await getCroppedImg(imageUrl, croppedAreaPixelsT, "Twitter"),
    };

    client.post("Process/begin", requestBody, {
      headers: {
        'Content-Type': 'application/json', // Use 'application/json' for JSON data
      },
    })
      .then(response => console.log("result put ", response))
      .catch(error => console.error("Error:", error)); // Use error parameter to catch errors
  };

  return (
    <Modal show={open} fullscreen={true} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>
          <Row>
            <Col xs={2}> <img style={{ width: '30px' }} src={cropLogo} /></Col>
            <Col xs={10}>  <h2> Crop Image</h2></Col>
          </Row>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>

        <Row>
          <Col xs={1}><img style={{ width: '35px' }} src={facebook} /></Col> {'\u00a0'}

          <Col xs={10}>  <h4 >Facebook</h4></Col>
        </Row><br></br>

        <Container>
        <Row>
        
          
        {
          imageLinks.map((link, index) => (
            
            <>
                
               
                  <Col xs="auto">
                    <Row>
                      <Cropper
                        image={link}
                        zoom={zoom}
                        crop={zoom}
                        aspect={1200 / 1400}
                        onCropChange={onCropChange}
                        onZoomChange={onZoomChange}
                        onCropComplete={onCropComplete}
                        style={
                          {
                            containerStyle: {
                              position: 'relative',
                              width: '370px', // Set the width to 500px
                              height: '370px', // Set the height to 500px
                            },

                            cropGuideStyle: {}, // You can customize the crop guide style if needed
                            mediaStyle: {},
                          }
                        }
                      />  </Row><br></br>
                    <Row>
                      <img style={{ width: '25px' }} src={zoomLogo} />{'\u00a0'}{'\u00a0'}{'\u00a0'}{'\u00a0'}{'\u00a0'}{'\u00a0'}{'\u00a0'}{'\u00a0'}{'\u00a0'}{'\u00a0'}
                      <input
                        type="range"
                        min={1}
                        max={3}
                        step={0.1}
                        value={zoom}
                        onInput={(e) => {
                          onZoomChange(e.target.value);
                        }}
                        className="slider"
                      ></input><br></br>
                    </Row>

                  </Col>
                  <Col xs="auto">
                    <VerticalLineWithShadow />
                  </Col>
                
              
                </>
                )
                )
        }
        
        </Row>
  </Container>

              </Modal.Body>

              <Modal.Footer >
                <Button style={{ width: '100px' }} variant="primary" onClick={() => { handleClose(); onBeginProcess() }}>
                  Crop
                </Button>
                <Button style={{ width: '100px' }} variant="secondary" onClick={() => { onCancel(); handleClose(); }}>
                  Cancel
                </Button>
              </Modal.Footer>
            </Modal>
          );
};

        export default Facebook;