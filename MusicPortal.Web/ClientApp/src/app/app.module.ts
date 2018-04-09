import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatListModule } from '@angular/material/list';

import { ArtistService } from './services/artist.service';
import { AlbumService } from './services/album.service';
import { TrackService } from './services/track.service';

import { ArtistComponent } from './artist/artist.component';
import { TrackComponent } from './track/track.component';
import { ArtistsComponent } from './chart/artists/artists.component';
import { TracksComponent } from './chart/tracks/tracks.component'
import { ArtistProfileComponent } from './artist-profile/artist-profile.component';
import { AlbumComponent } from './album/album.component';
import { AlbumPageComponent } from './album-page/album-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ArtistComponent,
    TrackComponent,
    ArtistsComponent,
    TracksComponent,
    ArtistProfileComponent,
    AlbumComponent,
    AlbumPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    MatIconModule,
    MatToolbarModule,
    MatMenuModule,
    MatListModule,
    MatTabsModule,
    MatButtonModule,
    MatGridListModule,
    FlexLayoutModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'chart/artists/:page', component: ArtistsComponent },
      { path: 'chart/tracks/:page', component: TracksComponent },
      { path: 'artist/:name', component: ArtistProfileComponent },
      { path: 'album/:name', component: AlbumPageComponent },
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    ArtistService,
    AlbumService,
    TrackService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
