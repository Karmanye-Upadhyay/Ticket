import { ModelClass } from 'src/app/model-class';
import { GlobalticketService } from '../globalticket.service';

import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetDetailsComponent } from '../get-details/get-details.component';
import { MatDialog } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-ticket-form',
  templateUrl: './ticket-form.component.html',
  styleUrls: ['./ticket-form.component.css'],
})
export class TicketFormComponent implements OnInit {
  @ViewChild('attachmentInput')
  attachmentInput!: ElementRef;

  loginForm!: FormGroup;
  imagesData: any;
  dataSource: any[] = [];
  data: boolean = false;
  formDataArray: any[] = [];
  datenow: any;
  url :string ="http://localhost:5023/api/Ticket/"
  imageUrl :string = "http://localhost:5023/api/Image/";

  loading: boolean = false;
  displayedColumns = [
    'id',
    'name',
    'category',
    'priority',
    'issue',
    'attachment',
    'status',
    'comment',
    'action',
  ];
  edit_form: boolean = false;
  formData: any;
  tempdata: string = '';
  extension: any;
  myFiles: string[] = [];
  sMsg: string = '';
  Idfordata: any;
  lastid: number = 0;
  downurl: any;
  selectedImages: any;
  edit: boolean = false;
  editImageArry: any;

  constructor(
    private fb: FormBuilder,
    public dialog: MatDialog,
    public service: GlobalticketService,
    private http: HttpClient
  ) {
    this.loadFunction();
  }

  ngOnInit() {
    // Initialize the form group
    this.loginForm = this.fb.group({
      ticket_Id: [0],
      ticket_Name: ['', Validators.required],
      category: ['', Validators.required],
      priority_Level: ['', Validators.required],
      issues: ['', Validators.required],
      attachment_Name: [''],
    });

    this.service.paymentList().subscribe({
      next: (res) => {
        this.lastid = res[res.length - 1].ticket_Id;
        this.Idfordata = this.lastid + 1;
        this.dataSource = res;
        console.log(this.Idfordata);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onSubmit() {
    console.log(this.loginForm.controls);
    if (this.loginForm.controls['ticket_Id'].value === 0) {
      const formDataTable = this.loginForm.value;
      console.log(this.tempdata);
      formDataTable.attachment = this.tempdata;

      this.service.addData(formDataTable).subscribe({
        next: (res: any) => {
          console.log('Form data sent successfully:', res);
          //image
          this.http
            .put<any>(this.imageUrl + "Imagedetail"+ this.Idfordata, this.formData)
            .subscribe({
              next: (response) => {
                console.log('Images uploaded successfully:', response);
              },
              error: (error) => {
                console.error('Error uploading images:', error);
                // Handle error if needed
              },
            });

          //getlist
          this.service.paymentList().subscribe({
            next: (res) => {
              // this.lastid=res[res.length - 1].ticket_Id
              // this.Idfordata =  this.lastid+1
              this.dataSource = res;
              console.log(res);
            },
            error: (err) => {
              console.log(err);
            },
          });
          this.loginForm.reset();
          this.selectedImages = [];
          const inputEl: HTMLInputElement = this.attachmentInput.nativeElement;
          inputEl.value = '';
        },
        error: (err) => {
          console.error('Error sending form data:', err);
        },
      });
    } 
    else {
      // const formDataTable1 = this.loginForm.value;
      // formDataTable1.attachment = this.tempdata;
      // formDataTable1.ticket_Id = this.loginForm.controls['ticket_id'].value;
      
      
      this.editTicketSubmit(this.loginForm.value);


      this.loginForm.reset();

          const inputEl: HTMLInputElement = this.attachmentInput.nativeElement;
          inputEl.value = '';
          this.editImageArry=[]
          this.selectedImages = [];
    }
    // } else {
    //   console.log('Form is invalid. Please fill all required fields.');
    // }
  }

  editTicketSubmit(element :any){
    const id = element.ticket_Id;

    const formDataTable = this.loginForm.value;
    (formDataTable.ticket_Id = element.ticket_Id)

    this.http.put(this.url+"UpdateTicketDetail" + id, formDataTable).subscribe({
      next: (res) => {
        this.service.paymentList().subscribe({
          next: (res) => {
            this.dataSource = res;
            this.http
            .put<any>(this.imageUrl+"UpdateImagedetail" +id, this.formData)
            .subscribe({
              next: (response) => {
                console.log('Images uploaded successfully:', response);
              },
              error: (error) => {
                console.error('Error uploading images:', error);
                // Handle error if needed
              },
            });

            console.log(res);
          },
          error: (err) => {
            console.log(err);
          },
        });
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
//
///
///
////
////
////
///
////
///
///
  handalImageUploadsingle(event: any) {
    console.log(event);

    const file = event.target.files[0];
    this.formData = new FormData();
    this.formData.append('formfile', file);
    console.log(this.formData);

    // formData.append('productcode', event.target.files[0].lastModified);
    // let code= event.target.files.name.value + event.target.files.lastModified.value
    this.http.put<any>(this.imageUrl +"Imagedetail", this.formData).subscribe({
      next: (response) => {
        this.tempdata = response.imagePath;
        this.extension = response.Extension;

        console.log('Image uploaded successfully:', response);
        // Handle response from backend if needed
      },
      error: (error) => {
        console.error('Error uploading image:', error);
        // Handle error if needed
      },
    });
  }

  // removeTicket(element: any) {
  //   const index = this.dataSource.indexOf(element);
  //   if (index !== -1) {
  //     this.dataSource.splice(index, 1);
  //     this.dataSource = [...this.dataSource];
  //   }
  // }

  removeTicket(element: any) {
    this.http.delete(this.url+"DeleteTicketDetail" + element.ticket_Id).subscribe({
      next: (res) => {
        this.service.paymentList().subscribe({
          next: (res) => {
            res.map((ele) => {
              ele.imageByte = 'data:image/jpeg;base64,' + ele.imageByte;
            });
            this.dataSource = res;
            console.log(res);
          },
          error: (err) => {
            console.log(err);
          },
        });
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  editTicket(element: any) {
    const id = element.ticket_Id;
    console.log("lllll"+element.multiAttachmentDtos);
    
    this.loginForm.patchValue({
      ticket_Id: element.ticket_Id,
      ticket_Name: element.ticket_Name,
      category: element.category,
      priority_Level: element.priority_Level,
      issues: element.issues,
      attachment_Name: '',
    });
    this.edit = true;
    const formDataTable = this.loginForm.value;
    (formDataTable.ticket_Id = element.ticket_Id),
      
        
        
    

    this.editImageArry! = [];
    if(element.multiAttachmentDtos.length!= undefined){
    for (let i = 0; i < element.multiAttachmentDtos.length; i++) {
      this.editImageArry[i] = element.multiAttachmentDtos[i].attachment_Name;
    }
    console.log(this.editImageArry);
  }
    
   

   
  }

  // this.removeTicket(item);

  openDialog = (element: any) => {
    console.log('1');

    let dialogRef = this.dialog.open(GetDetailsComponent, {
      width: '44%',
      height: '50%',
      data: element,
    });
    console.log('2');
    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`); // Pizza!
      console.log('3');
      const data1 = {
        ticket_Id: element.ticket_Id,
        ticket_Status: result.ticket_Status,
        comment: result.comment,
      };
      console.log('4');
      this.http
        .put(this.url + "UpdateComment" + element.ticket_Id, data1)
        .subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (err) => {
            console.log(err);
          },
        });
    });
  };

  getFileDetails(e: any) {
    //console.log (e.target.files);
    for (var i = 0; i < e.target.files.length; i++) {
      this.myFiles.push(e.target.files[i]);
    }
    console.log(this.myFiles);
  }

  uploadFiles() {
    const frmData = new FormData();

    for (var i = 0; i < this.myFiles.length; i++) {
      frmData.append('fileUpload', this.myFiles[i]);
    }
  }

  handalImageUpload(event: any) {
    console.log(event);
   
    const files: FileList = event.target.files;
    this.formData = new FormData();
    this.selectedImages = [];

    for (let i = 0; i < files.length; i++) {
      const file: File = files[i];
      this.formData.append('formfile', file);
      console.log(("ssssss"+this.formData));
      
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.selectedImages.push(e.target.result); // Add the data URL to selectedImages array
      };
      reader.readAsDataURL(file);
    }

    console.log(this.formData);
  }

  loadFunction() {
    this.loading = true;
    // Simulating loading time with setTimeout
    setTimeout(() => {
      this.loading = false;
    }, 5000); // Hides after 5 seconds
  }

 

  downloadImagesHandler = (elemets: any) => {
    // multiAttachmentDtos
    elemets.multiAttachmentDtos.forEach((ele: any, index: any) => {
      this.http
        .get(ele.attachment_Name, { responseType: 'blob' })
        .subscribe((response) => {
          const blob = new Blob([response], { type: 'image/jpg' }); // Adjust type as per your image type

          const link = document.createElement('a');
          link.href = window.URL.createObjectURL(blob);
          link.download = ele.orignalName + ele.extension; // Unique filename for each image

          link.click();

          window.URL.revokeObjectURL(link.href);
        });
    });
  };

  deleteImage = (image: any) => {
    if (this.editImageArry) {
      const index = this.editImageArry.indexOf(image);

      if (index !== -1) {
        this.editImageArry.splice(index, 1);
      }
      const parts = image.split('/');

      // Get the last part of the link
      const lastPart = parts[parts.length - 1];
      console.log(lastPart);

      this.http.delete(this.imageUrl+"DeleteImage" + lastPart).subscribe({
        next: (res) => {
          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
    if (this.selectedImages) {
      const index = this.selectedImages.indexOf(image);

      if (index !== -1) {
        this.selectedImages.splice(index, 1);
      }
    }
  };
}




 // downloadImage(id: number) {
  //   this.service.Download(id).subscribe({
  //     next: (res: any) => {
  //       const imageUrl: string =
  //         'http://localhost:4200/api/Image/GetMultipleImage3'; // Assuming res is a string URL

  //       // Fetch the image as a Blob
  //       fetch(imageUrl)
  //         .then((response) => response.blob())
  //         .then((blob) => {
  //           // Create a temporary anchor element
  //           const link = document.createElement('a');
  //           const url = window.URL.createObjectURL(blob);
  //           console.log(url);

  //           link.href = url;
  //           link.target = '_blank';
  //           link.download = 'hello.jpg'; // Set the filename for download

  //           // Trigger a click event on the anchor element to initiate download
  //           link.click();

  //           // Clean up
  //           window.URL.revokeObjectURL(url);
  //         })
  //         .catch((error) => {
  //           console.error('Error fetching image:', error);
  //         });
  //     },
  //     error: (err) => {
  //       console.error('Error downloading image:', err);
  //     },
  //   });
  // }
  // downloadUrl: string = 'http://localhost:5222/api/Image/GetMultipleImage';

  // download1(id: number): void {
  //   // Perform HTTP GET request to fetch image data
  //   this.http.get(this.downloadUrl + id).subscribe((res) => {
  //     // Create a Blob object from the response
  //     this.downurl = res;
  //     console.log(this.downurl);

  //     this.http
  //       .get('http://localhost:5222/Images/3/BatManImage.jpg', {
  //         responseType: 'blob',
  //       })
  //       .subscribe((response) => {
  //         const blob = new Blob([response], { type: 'image/jpg' });
  //         const link = document.createElement('a');
  //         link.href = window.URL.createObjectURL(blob);
  //         link.download = 'image.jpg'; // Adjust filename as needed

  //         // Simulate a click on the link to trigger download
  //         link.click();

  //         // Clean up
  //         window.URL.revokeObjectURL(link.href);
  //       });
  //     // Adjust type as per your image type

  //     // Create a download link
  //   });
  // }

  // download(url: string, index: number): void {
  //   // Perform HTTP GET request to fetch image data

  //   this.http.get(url, { responseType: 'blob' }).subscribe((response) => {
  //     // Create a Blob object from the response
  //     const blob = new Blob([response], { type: 'image/jpg' }); // Adjust type as per your image type

  //     // Create a download link
  //     const link = document.createElement('a');
  //     link.href = window.URL.createObjectURL(blob);
  //     link.download = 'ss'; // Unique filename for each image

  //     // Simulate a click on the link to trigger download
  //     link.click();

  //     // Clean up
  //     window.URL.revokeObjectURL(link.href);
  //   });
  // }