import { Component, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-get-details',
  templateUrl: './get-details.component.html',
  styleUrls: ['./get-details.component.css'],
})
export class GetDetailsComponent {
  constructor(
    public dialogRef: MatDialogRef<GetDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {
    console.log(data);
  }
  loginForm: any;

  ngOnInit() {
    // Initialize the form group
    this.loginForm = this.fb.group({
      ticket_Status: [''],
      comment: [''],
    });
  }
  saveCommentHandler = () => {
    // Update the status of the item
    this.data.ticket_Status = this.loginForm.get('ticket_Status').value;
    this.data.comment = this.loginForm.get('comment').value;
    console.log('111');
    console.log('112');

    this.dialogRef.close(this.data);
  };
}
