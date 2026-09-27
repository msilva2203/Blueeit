import ThreadNode from './ThreadNode';

import "./ThreadList.css";

export default function ThreadList({threads}) {
    return (
        <>
            { threads.map((thread) => (
                <div>
                    <ThreadNode thread={thread} />
                </div>
            ))}
        </>
    );
}