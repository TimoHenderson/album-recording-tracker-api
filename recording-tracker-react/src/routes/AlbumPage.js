import { useParams } from 'react-router-dom';
import Album from '../components/Elements/Album';
import Song from '../components/Elements/Song';
import ElementList from '../components/Lists/ElementList';

const AlbumPage = ({ recordingData }) => {
    const { artistId, albumId } = useParams();
    const artist = recordingData.find((artist) => artist.id === Number(artistId));
    const album = artist ? artist.albums.find((album) => album.id === Number(albumId)) : null;
    const songNodes = album ?
        album.songs.map((song) => <Song key={song.id} song={song} album={album} artist={artist} />)
        : null;

    return (
        <>
            {artist && <h2>{artist.name}</h2>}
            {album && <Album artist={artist} album={album} expanded={true} />}
            <ElementList listNodes={songNodes} elementName={"Songs"} />
        </>
    );
}

export default AlbumPage;