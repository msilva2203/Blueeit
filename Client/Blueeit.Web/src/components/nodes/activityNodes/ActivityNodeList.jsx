import ActivityNode from "./ActivityNode";

import "./ActivityNodeList.css";

export default function ActivityNodeList({activities}) {
    return (
        <>
            <div className="blueeit-activitynodelist">
                { activities.map((activity, index) => (
                    <div className="blueeit-activitynodelist-item">
                        <ActivityNode activity={activity} />
                    </div>
                )) }
            </div>
        </>
    );
}